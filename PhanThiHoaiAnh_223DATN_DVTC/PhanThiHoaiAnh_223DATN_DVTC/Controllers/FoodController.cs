using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
	public class FoodController : Controller
	{
		private readonly DataContext _dataContext;
		public FoodController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public IActionResult Index()
		{
			var food = _dataContext.FoodModel.Include("FoodSequence").Include("FoodCategory").ToList();

			return View(food); 
		}
		public async Task<IActionResult> Detail(int Id)
		{
			if (Id == null) return RedirectToAction("Index");
			var serviceById = _dataContext.FoodModel.Where(s => s.Id == Id).FirstOrDefault();

			return View(serviceById);
		}
	}
}
