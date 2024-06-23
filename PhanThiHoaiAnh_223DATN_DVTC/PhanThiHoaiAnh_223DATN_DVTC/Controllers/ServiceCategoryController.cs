using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
	public class ServiceCategoryController : Controller
	{
        private readonly DataContext _dataContext;
		public ServiceCategoryController(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IActionResult> Index(String Slug ="")
        { 
			ServiceCategoryModel serviceCate = _dataContext.tblServiceCategories.Where(c => c.Slug == Slug).FirstOrDefault();
			if (serviceCate == null) return RedirectToAction("Index");
			
			var serviceByCategory = _dataContext.tblOtherServices.Where(s => s.CategoryId == serviceCate.Id);
			
			return View(await serviceByCategory.OrderByDescending(s => s.Id).ToListAsync()); 
		}
    }
}
