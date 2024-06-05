using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MenuController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Menus.OrderByDescending(s => s.Id).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuModel food)
        {
            if (ModelState.IsValid)
            {
                //them du lieu
                food.Slug = food.Name.Replace(" ", "-");
                var slug = await _dataContext.Menus.FirstOrDefaultAsync(f => f.Slug == food.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Thực đơn đã tồn tại");
                    return View(food);
                }
                if (food.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/food");
                    string imageName = Guid.NewGuid().ToString() + "_" + food.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await food.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    food.Image = imageName;
                }
                _dataContext.Add(food);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm thực đơn thành công";
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
            MenuModel food = await _dataContext.Menus.FindAsync(Id);
            return View(food);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, MenuModel food)
        {
            if (ModelState.IsValid)
            {
                //them du lieu
                food.Slug = food.Name.Replace(" ", "-");
                if (food.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/food");
                    string imageName = Guid.NewGuid().ToString() + "_" + food.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await food.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    food.Image = imageName;
                }
                _dataContext.Update(food);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Cập nhật thực đơn thành công";
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
            MenuModel food = await _dataContext.Menus.FindAsync(Id);
            if (!string.Equals(food.Image, "noimage.jpg"))
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/food");
                string oldfileImg = Path.Combine(uploadDir, food.Image);
                if (System.IO.File.Exists(oldfileImg))
                {
                    System.IO.File.Delete(oldfileImg);
                }
            }
            _dataContext.Menus.Remove(food);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Thực đơn đã được xóa";
            return RedirectToAction("Index");
        }
    }
}
