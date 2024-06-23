using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
	public class FoodCategoryController : Controller
	{
		private readonly DataContext _dataContext;
		public FoodCategoryController(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IActionResult> Index(String Slug = "")
		{
			FoodCategoryModel serviceCate = _dataContext.tblFoodCategories.Where(c => c.Slug == Slug).FirstOrDefault();
			if (serviceCate == null) return RedirectToAction("Index");

			var serviceByCategory = _dataContext.tblFood.Where(s => s.FoodCategoryId == serviceCate.Id);

			return View(await serviceByCategory.OrderByDescending(s => s.Id).Include("FoodCategory").Include("FoodSequence").ToListAsync());
		}
	}
}
