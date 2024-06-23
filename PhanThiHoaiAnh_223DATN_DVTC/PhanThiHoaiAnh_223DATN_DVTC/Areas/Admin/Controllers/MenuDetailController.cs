using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class MenuDetailController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MenuDetailController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.tblMenuDetails.OrderByDescending(s => s.Id).Include(s=> s.Menu).Include(s=> s.Food).ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.Menus = new SelectList(_dataContext.tblMenu, "Id", "Name");
            ViewBag.FoodModel = new SelectList(_dataContext.tblFood, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuDetail food)
        {
            ViewBag.Menus = new SelectList(_dataContext.tblMenu, "Id", "Name", food.MenuId);
            ViewBag.FoodModel = new SelectList(_dataContext.tblFood, "Id", "Name", food.FoodId);
            if (ModelState.IsValid)
            {
                //them du lieu
                var slug = await _dataContext.tblMenuDetails.FirstOrDefaultAsync(f => f.Id == food.Id);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Chi tiết thực đơn đã tồn tại");
                    return View(food);
                }
                _dataContext.Add(food);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm Chi tiết thực đơn thành công";
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
        public async Task<IActionResult> Edit(int Id)
        {
            MenuDetail food = await _dataContext.tblMenuDetails.FindAsync(Id);
            ViewBag.Menus = new SelectList(_dataContext.tblMenu, "Id", "Name", food.MenuId);
            ViewBag.FoodModel = new SelectList(_dataContext.tblFood, "Id", "Name", food.FoodId);
            return View(food);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, MenuDetail food)
        {
            ViewBag.Menus = new SelectList(_dataContext.tblMenu, "Id", "Name", food.MenuId);
            ViewBag.FoodModel = new SelectList(_dataContext.tblFood, "Id", "Name", food.FoodId);
            if (ModelState.IsValid)
            {
                _dataContext.Update(food);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Cập nhật Chi tiết thực đơn thành công";
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
        public async Task<IActionResult> Delete(int Id)
        {
            MenuDetail food = await _dataContext.tblMenuDetails.FindAsync(Id);
            _dataContext.tblMenuDetails.Remove(food);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Chi tiết thực đơn đã được xóa";
            return RedirectToAction("Index");
        }
    }
}
