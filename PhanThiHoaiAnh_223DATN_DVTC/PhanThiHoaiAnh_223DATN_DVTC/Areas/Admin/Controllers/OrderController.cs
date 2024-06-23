
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;
using SelectPdf;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
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

            //if (order != null && order.Detail != null)
            //{
            //    var menuDetails = await _dataContext.tblMenuDetails
            //	.Where(md => md.MenuId == order.Detail.ServiceId)
            //	.ToListAsync();
            // Lấy danh sách chi tiết món ăn từ tblMenuDetail dựa vào MenuId của Menu liên kết với đơn hàng

            //  ViewBag.MenuDetails = menuDetails; // Truyền danh sách chi tiết món ăn vào ViewBag

            //  return View("Detail", order);
            //}

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
		public async Task<IActionResult> Update(int Id, DatTiecModel food)
		{
			if (ModelState.IsValid)
			{
				_dataContext.Update(food);
				await _dataContext.SaveChangesAsync();
				TempData["success"] = "Cập nhật trạng thái dịch vụ thành công";
				return RedirectToAction("Index");
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
			return View(food);
		}
	}
}
