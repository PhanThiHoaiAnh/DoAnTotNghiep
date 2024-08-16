
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;
using PhanThiHoaiAnh_223DATN_DVTC.Services;
using SelectPdf;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin, Member")]
    public class OrderController : Controller
	{
		private readonly DataContext _dataContext;
		public OrderController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
        public async Task<IActionResult> Index()
		{
			var order = _dataContext.tblOrder.ToList();
			return View(order);
		}

        public async Task<IActionResult> ViewOrder(string ordercode)
        {
			var DetailsOrder = await _dataContext.tblOrderDetails.Include(od => od.Service).Where(od => od.OrderCode==ordercode).ToListAsync();
            return View(DetailsOrder);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var order = await _dataContext.tblOrder.FirstOrDefaultAsync(s => s.Id == id);
            if (order != null)
            {
                return View("Detail", order);
            }
            return View("Detail");
        }
        public async Task<IActionResult> Delete(int Id)
        {
            DatTiecModel food = await _dataContext.tblOrder.FindAsync(Id);
            _dataContext.tblOrder.Remove(food);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Đơn dịch vụ đã được xóa";
            return RedirectToAction("Index");
        }
        public IActionResult GeneratePdf(string html)
        {
            html = html.Replace("StrTag", "<").Replace("EndTag", ">");
            HtmlToPdf oHtmlToPdf = new HtmlToPdf();
            PdfDocument oPdfDocument = oHtmlToPdf.ConvertHtmlString(html);
            byte[] pdf = oPdfDocument.Save();
            oPdfDocument.Close();

			return File(
                pdf,
                "application/pdf",
                "HoaDon.pdf"
             );
		}

		public async Task<IActionResult> Update(int Id)
		{
			DatTiecModel food = await _dataContext.tblOrder.FindAsync(Id);
			return View(food);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Update(int Id, DatTiecModel updatedFood)
		{
			if (ModelState.IsValid)
			{
                var existingFood = await _dataContext.tblOrder.FindAsync(Id);
                if (existingFood != null)
                {
                    // Gắn kết đối tượng updatedFood vào ngữ cảnh (_dataContext)
                    _dataContext.Attach(existingFood);

                    // Cập nhật các trường mong muốn
                    existingFood.FtName = updatedFood.FtName;
                    existingFood.LtName = updatedFood.LtName;
                    existingFood.OrderOrg = updatedFood.OrderOrg;
                    existingFood.PhoneNumber = updatedFood.PhoneNumber;
                    existingFood.Address = updatedFood.Address;
                    existingFood.Status = updatedFood.Status;
                    existingFood.Payment = updatedFood.Payment;
                    
                    await _dataContext.SaveChangesAsync();
                    TempData["success"] = "Cập nhật trạng thái dịch vụ thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Không tìm thấy đối tượng dữ liệu";
                    return NotFound();
                }
			}
			else
			{
				TempData["error"] = "Model có một vài thứ đang bị lỗi";
				List<string> errors = new List<string>();
				foreach (var value in ModelState.Values)
				{
					foreach (var error in value.Errors)
					{
						errors.Add(error.ErrorMessage);
					}
				}
				string errorMessage = string.Join("\n", errors);
				return BadRequest(errorMessage);
			}
			return View(updatedFood);
		}
	}
}
