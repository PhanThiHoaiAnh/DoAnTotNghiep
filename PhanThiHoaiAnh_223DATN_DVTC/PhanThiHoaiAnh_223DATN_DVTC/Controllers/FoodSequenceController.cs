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
			FoodSequenceModel foodSq = _dataContext.FoodSequence.Where(s => s.Slug == Slug).FirstOrDefault();

			var foodBySequence = _dataContext.FoodModel.Where(f => f.Id == foodSq.Id);

			return View(await foodBySequence.OrderByDescending(f => f.Id).ToListAsync());
		}

    }
}
