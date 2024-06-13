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
    public class LocationController : Controller
    {
        private readonly DataContext _dataContext;
        public LocationController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Location.OrderByDescending(s => s.Id).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocationModel lc)
        {
            if (ModelState.IsValid)
            {
                //them du lieu
                lc.Slug = lc.Name.Replace(" ", "-");
                var slug = await _dataContext.FoodCategories.FirstOrDefaultAsync(f => f.Slug == lc.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Loại tiệc đã tồn tại");
                    return View(lc);
                }
                _dataContext.Add(lc);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm địa điểm thành công";
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

            return View(lc);
        }
        public async Task<IActionResult> Edit(int Id)
        {
            LocationModel food = await _dataContext.Location.FindAsync(Id);
            return View(food);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, LocationModel lc)
        {
            if (ModelState.IsValid)
            {
                //them du lieu
                lc.Slug = lc.Name.Replace(" ", "-");
                _dataContext.Update(lc);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Cập nhật địa điểm thành công";
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
            return View(lc);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            LocationModel food = await _dataContext.Location.FindAsync(Id);
            _dataContext.Location.Remove(food);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Địa điểm đã được xóa";
            return RedirectToAction("Index");
        }
    }
}
