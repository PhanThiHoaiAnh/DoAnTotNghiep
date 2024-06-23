using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class WeddingCardController : Controller
	{
		private readonly DataContext _dataContext;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public WeddingCardController(DataContext context, IWebHostEnvironment webHostEnvironment)
		{
			_dataContext = context;
			_webHostEnvironment = webHostEnvironment;

		}
		public async Task<IActionResult> Index()
		{
			return View(await _dataContext.tblWeddingCardCategories.OrderByDescending(s => s.Id).ToListAsync());
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(WeddingCardCategoryModel service)
		{
			if (ModelState.IsValid)
			{
				//them du lieu
				service.Slug = service.Name.Replace(" ", "-");
				var slug = await _dataContext.tblWeddingCardCategories.FirstOrDefaultAsync(s => s.Slug == service.Slug);
				if (slug != null)
				{
					ModelState.AddModelError("", "Thiệp đã tồn tại");
					return View(service);
				}
				if (service.ImageUpload != null)
				{
					string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/services");
					string imageName = Guid.NewGuid().ToString() + "_" + service.ImageUpload.FileName;
					string filePath = Path.Combine(uploadDir, imageName);

					FileStream fs = new FileStream(filePath, FileMode.Create);
					await service.ImageUpload.CopyToAsync(fs);
					fs.Close();
					service.Image = imageName;
				}
				_dataContext.Add(service);
				await _dataContext.SaveChangesAsync();
				TempData["success"] = "Thêm thiệp thành công";
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

			return View(service);
		}
		public async Task<IActionResult> Edit(int Id)
		{
			WeddingCardCategoryModel service = await _dataContext.tblWeddingCardCategories.FindAsync(Id);
			return View(service);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int Id, WeddingCardCategoryModel service)
		{
			if (ModelState.IsValid)
			{
				//them du lieu
				service.Slug = service.Name.Replace(" ", "-");
				var slug = await _dataContext.tblWeddingCardCategories.FirstOrDefaultAsync(s => s.Slug == service.Slug);
				
				if (service.ImageUpload != null)
				{
					string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/services");
					string imageName = Guid.NewGuid().ToString() + "_" + service.ImageUpload.FileName;
					string filePath = Path.Combine(uploadDir, imageName);

					FileStream fs = new FileStream(filePath, FileMode.Create);
					await service.ImageUpload.CopyToAsync(fs);
					fs.Close();
					service.Image = imageName;
				}
				_dataContext.Update(service);
				await _dataContext.SaveChangesAsync();
				TempData["success"] = "Cập nhật thiệp thành công";
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
			return View(service);
		}
		public async Task<IActionResult> Delete(int Id)
		{
			WeddingCardCategoryModel service = await _dataContext.tblWeddingCardCategories.FindAsync(Id);
			if (!string.Equals(service.Image, "noimage.jpg"))
			{
				string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/services");
				string oldfileImg = Path.Combine(uploadDir, service.Image);
				if (System.IO.File.Exists(oldfileImg))
				{
					System.IO.File.Delete(oldfileImg);
				}
			}
			_dataContext.tblWeddingCardCategories.Remove(service);
			await _dataContext.SaveChangesAsync();
			TempData["error"] = "Dịch vụ đã được xóa";
			return RedirectToAction("Index");
		}
	}
}
