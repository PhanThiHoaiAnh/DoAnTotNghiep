using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TableCategoryController : Controller
    {
        private readonly DataContext _dataContext;
        public TableCategoryController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.TableCategories.OrderByDescending(s => s.Id).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TableCategoryModel tbl)
        {
            if (ModelState.IsValid)
            {
                //them du lieu
                var code = await _dataContext.TableCategories.FirstOrDefaultAsync(f => f.Id == tbl.Id);
                if (code != null)
                {
                    ModelState.AddModelError("", "Loại tiệc đã tồn tại");
                    return View(tbl);
                }
                _dataContext.Add(tbl);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm Loại bàn tiệc thành công";
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

            return View(tbl);
        }
        public async Task<IActionResult> Edit(int Id)
        {
            TableCategoryModel tbl = await _dataContext.TableCategories.FindAsync(Id);
            return View(tbl);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, TableCategoryModel tbl)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Update(tbl);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Cập nhật Loại bàn tiệc thành công";
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
            return View(tbl);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            TableCategoryModel tbl = await _dataContext.TableCategories.FindAsync(Id);
            _dataContext.TableCategories.Remove(tbl);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Loại bàn tiệc đã được xóa";
            return RedirectToAction("Index");
        }
    }
}
