﻿using Microsoft.AspNetCore.Authorization;
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
    public class FoodController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FoodController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.FoodModel.OrderByDescending(s => s.Id).Include(s=> s.FoodSequence).Include(s=> s.FoodCategory).ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.FoodCategories = new SelectList(_dataContext.FoodCategories, "Id", "CategoryName");
            ViewBag.FoodSequence = new SelectList(_dataContext.FoodSequence, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodModel food)
        {
            ViewBag.FoodCategories = new SelectList(_dataContext.FoodCategories, "Id", "CategoryName", food.FoodCategoryId);
            ViewBag.FoodSequence = new SelectList(_dataContext.FoodSequence, "Id", "Name", food.FoodSequenceId);

            if (ModelState.IsValid)
            {
                //them du lieu
                food.Slug = food.Name.Replace(" ", "-");
                var slug = await _dataContext.FoodCategories.FirstOrDefaultAsync(f => f.Slug == food.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Món ăn đã tồn tại");
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
                TempData["success"] = "Thêm món ăn thành công";
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
            FoodModel food = await _dataContext.FoodModel.FindAsync(Id);
            ViewBag.FoodCategories = new SelectList(_dataContext.FoodCategories, "Id", "CategoryName", food.FoodCategoryId);
            ViewBag.FoodSequence = new SelectList(_dataContext.FoodSequence, "Id", "Name", food.FoodSequenceId);

            return View(food);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, FoodModel food)
        {
            ViewBag.FoodCategories = new SelectList(_dataContext.FoodCategories, "Id", "CategoryName", food.FoodCategoryId);
            ViewBag.FoodSequence = new SelectList(_dataContext.FoodSequence, "Id", "Name", food.FoodSequenceId);

            if (ModelState.IsValid)
            {
                //them du lieu
                food.Slug = food.Name.Replace(" ", "-");
                var slug = await _dataContext.FoodModel.FirstOrDefaultAsync(f => f.Slug == food.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Món ăn đã tồn tại");
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
                _dataContext.Update(food);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Cập nhật món ăn thành công";
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
            FoodModel food = await _dataContext.FoodModel.FindAsync(Id);
            if (!string.Equals(food.Image, "noimage.jpg"))
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/food");
                string oldfileImg = Path.Combine(uploadDir, food.Image);
                if (System.IO.File.Exists(oldfileImg))
                {
                    System.IO.File.Delete(oldfileImg);
                }
            }
            _dataContext.FoodModel.Remove(food);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Dịch vụ đã được xóa";
            return RedirectToAction("Index");
        }

    }
}
