using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Models.Views;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MenuController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MenuController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(List<FoodModel> selectedFoodItems)
        {
            // Sử dụng danh sách các món ăn đã chọn để hiển thị trên trang Index
            ViewBag.SelectedFoodItems = selectedFoodItems;

            return View(await _dataContext.tblMenu.OrderByDescending(s => s.Id).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult CreateMenu()
        {
            // Lấy danh sách món ăn từ cơ sở dữ liệu
            List<FoodModel> availableFoodItems = _dataContext.tblFood.ToList();

            MenuModel model = new MenuModel
            {
                FoodItems = availableFoodItems
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMenu(MenuModel menuModel)
        {
			if (ModelState.IsValid)
			{
				menuModel.Slug = menuModel.Name.Replace(" ", "-");
                var slug = await _dataContext.tblMenu.FirstOrDefaultAsync(f => f.Slug == menuModel.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Thực đơn đã tồn tại");
                    return View(menuModel);
                }
                // Lấy danh sách các món ăn đã chọn
                List<FoodModel> selectedFoodItems = _dataContext.tblFood
                    .Where(fi => menuModel.SelectedFoodItems.Contains(fi.Id))
                    .ToList();
                
                // Tính tổng giá của các món ăn đã chọn
                long totalPrice = selectedFoodItems.Sum(fi => fi.Price);

                // Tạo biến selectedFoodItemsString và gán giá trị
                string selectedFoodItemsString = string.Join(",", menuModel.SelectedFoodItems);

                // Chuyển đổi danh sách các ID món ăn từ chuỗi thành List<int>
                List<int> parsedSelectedFoodItems = selectedFoodItemsString
                    .Split(',')
                    .Select(int.Parse)
                    .ToList();

                // Lưu vào cơ sở dữ liệu
                MenuModel menu = new MenuModel
                {
                    Name = menuModel.Name,
                    Slug = menuModel.Name.Replace(" ", "-"),
                    Description = menuModel.Description,
                    Status = menuModel.Status,
                    SelectedFoodItems = parsedSelectedFoodItems,
                    Price = totalPrice,
                };
                // Lấy danh sách tên các món ăn đã chọn
                List<string> selectedFoodItemNames = _dataContext.tblFood
                    .Where(fi => menu.SelectedFoodItems.Contains(fi.Id))
                    .Select(fi => fi.Name)
                    .ToList();

                // Gán danh sách tên các món ăn vào menu.SelectedFoodItemNames
                menu.SelectedFoodItemNames = selectedFoodItemNames;
                if (menuModel.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/food");
                    string imageName = Guid.NewGuid().ToString() + "_" + menuModel.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await menuModel.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    menu.Image = imageName;
                }
                _dataContext.tblMenu.Add(menu);
                _dataContext.SaveChanges();

                return RedirectToAction("Index"); // Chuyển hướng đến trang chủ hoặc trang khác tùy ý
            }

            // Nếu ModelState không hợp lệ, quay trở lại trang CreateMenu với model để hiển thị thông báo lỗi
            menuModel.FoodItems = _dataContext.tblFood.ToList();
            return View(menuModel);
        }

        // Action để hiển thị chi tiết một thực đơn
        public IActionResult MenuDetails(int id)
        {
            // Lấy thông tin thực đơn từ cơ sở dữ liệu bằng id
            MenuModel menu = _dataContext.tblMenu.Include(m => m.FoodItems).FirstOrDefault(m => m.Id == id);

            if (menu == null)
            {
                return NotFound(); // Xử lý trường hợp thực đơn không tồn tại
            }

            // Trả về view hiển thị chi tiết thực đơn
            return View(menu);
        }

        // Helper method để lấy danh sách món ăn từ danh sách id
        private List<FoodModel> GetFoodItemsById(List<int> foodItemIds)
        {
            // Lấy danh sách món ăn từ cơ sở dữ liệu bằng các id
            List<FoodModel> foodItems = _dataContext.tblFood.Where(f => foodItemIds.Contains(f.Id)).ToList();

            return foodItems;
        }
        public IActionResult Edit(int id)
        {
            MenuModel menu = _dataContext.tblMenu.FirstOrDefault(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            List<FoodModel> availableFoodItems = _dataContext.tblFood.ToList();

            MenuModel model = new MenuModel
            {
                Id = menu.Id,
                Name = menu.Name,
                Description = menu.Description,
                Status = menu.Status,
                SelectedFoodItems = menu.SelectedFoodItems,
                FoodItems = availableFoodItems
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, MenuModel menuModel)
        {
            if (ModelState.IsValid)
            {
                MenuModel menu = _dataContext.tblMenu.FirstOrDefault(m => m.Id == menuModel.Id);
                if (menu == null)
                {
                    return NotFound();
                }
                if (menuModel.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/food");
                    string imageName = Guid.NewGuid().ToString() + "_" + menuModel.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await menuModel.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    menuModel.Image = imageName;
                }
                List<FoodModel> selectedFoodItems = _dataContext.tblFood
                    .Where(fi => menuModel.SelectedFoodItems.Contains(fi.Id))
                    .ToList();

                // Tính tổng giá của các món ăn đã chọn
                long totalPrice = selectedFoodItems.Sum(fi => fi.Price);
                // Tạo biến selectedFoodItemsString và gán giá trị
                string selectedFoodItemsString = string.Join(",", menuModel.SelectedFoodItems);

                // Chuyển đổi danh sách các ID món ăn từ chuỗi thành List<int>
                List<int> parsedSelectedFoodItems = selectedFoodItemsString
                    .Split(',')
                    .Select(int.Parse)
                    .ToList();
                // Cập nhật thông tin thực đơn
                menu.Name = menuModel.Name;
                menu.Description = menuModel.Description;
                menu.Status = menuModel.Status;
                menu.SelectedFoodItems = parsedSelectedFoodItems;
                menu.Price = totalPrice;
                List<string> selectedFoodItemNames = _dataContext.tblFood
                    .Where(fi => menu.SelectedFoodItems.Contains(fi.Id))
                    .Select(fi => fi.Name)
                    .ToList();

                // Gán danh sách tên các món ăn vào menu.SelectedFoodItemNames
                menu.SelectedFoodItemNames = selectedFoodItemNames;

                // Các bước xử lý khác

                _dataContext.tblMenu.Update(menu);
                _dataContext.SaveChanges();

                return RedirectToAction("Index"); // Chuyển hướng đến trang chủ hoặc trang khác tùy ý
            }

            // Nếu ModelState không hợp lệ, quay trở lại trang EditMenu với model để hiển thị thông báo lỗi
            menuModel.FoodItems = _dataContext.tblFood.ToList();
            return View(menuModel);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            MenuModel food = await _dataContext.tblMenu.FindAsync(Id);
            if (!string.Equals(food.Image, "noimage.jpg"))
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/food");
                string oldfileImg = Path.Combine(uploadDir, food.Image);
                if (System.IO.File.Exists(oldfileImg))
                {
                    System.IO.File.Delete(oldfileImg);
                }
            }
            _dataContext.tblMenu.Remove(food);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Thực đơn đã được xóa";
            return RedirectToAction("Index");
        }
    }
}
