using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceCategoryController : Controller
    {
        private readonly DataContext _dataContext;
        public ServiceCategoryController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.ServiceCategories.OrderByDescending(s => s.Id).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceCategoryModel serviceCategory)
        {
            if (ModelState.IsValid)
            {
                //them du lieu
                serviceCategory.Slug = serviceCategory.CategoryName.Replace(" ", "-");
                var slug = await _dataContext.ServiceCategories.FirstOrDefaultAsync(s => s.Slug == serviceCategory.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Loại dịch vụ đã tồn tại");
                    return View(serviceCategory);
                }

                _dataContext.Add(serviceCategory);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm thành công";
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
            return View(serviceCategory);
        }
        public async Task<IActionResult> Edit(string Id)
        {
            ServiceCategoryModel serviceCategory = await _dataContext.ServiceCategories.FindAsync(Id);
            return View(serviceCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServiceCategoryModel serviceCategory)
        {
            if (ModelState.IsValid)
            {
                //them du lieu
                serviceCategory.Slug = serviceCategory.CategoryName.Replace(" ", "-");
                var slug = await _dataContext.ServiceCategories.FirstOrDefaultAsync(s => s.Slug == serviceCategory.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Loại dịch vụ đã tồn tại");
                    return View(serviceCategory);
                }

                _dataContext.Update(serviceCategory);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Cập nhật thành công";
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
            return View(serviceCategory);
        }
        public async Task<IActionResult> Delete(string Id)
        {
            ServiceCategoryModel serviceCategory = await _dataContext.ServiceCategories.FindAsync(Id);

            _dataContext.ServiceCategories.Remove(serviceCategory);
            await _dataContext.SaveChangesAsync();
            TempData["success"] = "Loại dịch vụ đã được xóa";
            return RedirectToAction("Index");
        }
    }
}
