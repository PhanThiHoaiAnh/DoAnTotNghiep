using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models.Views;
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
			var food = _dataContext.tblFood.Include("FoodSequence").Include("FoodCategory").ToList();

			return View(food); 
		}
		public async Task<IActionResult> Detail(int Id)
		{
			var data = _dataContext.tblFood.Include(p => p.FoodSequence)
											.Include(p => p.FoodCategory)
											.SingleOrDefault(p => p.Id == Id);
			if (data == null) return RedirectToAction("Index");
			
			var result = new FoodDetailViewModel
			{
				Id = data.Id,
				Name = data.Name,
				Price = data.Price,
				Image = data.Image,
				Description = data.Description,
				Category = data.FoodCategory.CategoryName,
				Sequence = data.FoodSequence.Name
			};
			return View(result);
		}
	}
}
