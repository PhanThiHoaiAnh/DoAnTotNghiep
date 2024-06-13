using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FoodCategoryController : Controller
    {
        private readonly DataContext _dataContext;
        public FoodCategoryController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.FoodCategories.OrderByDescending(s => s.Id).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodCategoryModel food)
        {
            if (ModelState.IsValid)
            {
                //them du lieu
                food.Slug = food.CategoryName.Replace(" ", "-");
                var slug = await _dataContext.FoodCategories.FirstOrDefaultAsync(f => f.Slug == food.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Loại món đã tồn tại");
                    return View(food);
                }
                _dataContext.Add(food);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm loại món thành công";
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
        public async Task<IActionResult> Edit(string Id)
        {
            FoodCategoryModel food = await _dataContext.FoodCategories.FindAsync(Id);
            return View(food);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id, FoodCategoryModel food)
        {
            if (ModelState.IsValid)
            {
                //them du lieu
                food.Slug = food.CategoryName.Replace(" ", "-");
                _dataContext.Update(food);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Cập nhật dịch vụ thành công";
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
        public async Task<IActionResult> Delete(string Id)
        {
            FoodCategoryModel food = await _dataContext.FoodCategories.FindAsync(Id);
            _dataContext.FoodCategories.Remove(food);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Loại món đã được xóa";
            return RedirectToAction("Index");
        }
    }
}
