using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PositionController : Controller
    {
        private readonly DataContext _dataContext;
        public PositionController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Positions.OrderByDescending(s => s.Id).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PositionModel lc)
        {
            if (ModelState.IsValid)
            {
                //them du lieu
                lc.Slug = lc.Name.Replace(" ", "-");
                var slug = await _dataContext.Positions.FirstOrDefaultAsync(f => f.Slug == lc.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Chúc vụ đã tồn tại");
                    return View(lc);
                }
                _dataContext.Add(lc);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm chức vụ thành công";
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
        public async Task<IActionResult> Edit(string Id)
        {
            PositionModel food = await _dataContext.Positions.FindAsync(Id);
            return View(food);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id, PositionModel lc)
        {
            if (ModelState.IsValid)
            {
                //them du lieu
                lc.Slug = lc.Name.Replace(" ", "-");
                _dataContext.Update(lc);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Cập nhật chức vụ thành công";
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
        public async Task<IActionResult> Delete(string Id)
        {
            PositionModel food = await _dataContext.Positions.FindAsync(Id);
            _dataContext.Positions.Remove(food);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Chức vụ đã được xóa";
            return RedirectToAction("Index");
        }
    }
}
