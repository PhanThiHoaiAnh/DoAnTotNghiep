using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
	public class FoodSequenceController : Controller
	{
		private readonly DataContext _dataContext;
		public FoodSequenceController(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IActionResult> Index(String Slug = "")
		{
			FoodSequenceModel serviceCate = _dataContext.FoodSequence.Where(c => c.Slug == Slug).FirstOrDefault();
			if (serviceCate == null) return RedirectToAction("Index");

			var serviceByCategory = _dataContext.FoodModel.Where(s => s.FoodSequenceId == serviceCate.Id);

			return View(await serviceByCategory.OrderByDescending(s => s.Id).Include("FoodCategory").Include("FoodSequence").ToListAsync());
		}

	}
}
