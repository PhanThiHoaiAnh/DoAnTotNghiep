using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
	public class OtherServicesController :Controller
	{
		private readonly DataContext _dataContext;
		public OtherServicesController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async Task<IActionResult> Index()
		{  
			return View(await _dataContext.OtherServices.OrderByDescending(s => s.Id).Include("Category").ToListAsync()); 
		}

		public async Task<IActionResult> Detail(int Id)
		{ 
			if (Id ==null) return RedirectToAction("Index");
			var serviceById = _dataContext.OtherServices.Where(s => s.Id == Id).FirstOrDefault();

			return View( serviceById);
		}
	}
}
