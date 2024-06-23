using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models.Views;
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
			return View(await _dataContext.tblOtherServices.OrderByDescending(s => s.Id).Include("Category").ToListAsync()); 
		}
		public async Task<IActionResult> Detail(int Id)
		{ 
			var data = _dataContext.tblOtherServices.Include(p => p.Category).SingleOrDefault(p => p.Id == Id);
			if (data ==null) return RedirectToAction("Index");
			//var serviceById = _dataContext.OtherServices.Where(s => s.Id == Id).FirstOrDefault();
			var result = new ServiceDetailViewModel
			{
				Id = data.Id,
				Name = data.Name,
				Price = data.Price,
				Image = data.Image,
				Description = data.Description,
				Category = data.Category.CategoryName
			};
			var recommendations = await _dataContext.tblOtherServices
				.Where(p => p.Id != Id)
				.OrderBy(r => Guid.NewGuid()) // Lấy ngẫu nhiên các dịch vụ đề xuất
				.Take(3) // Giới hạn số lượng dịch vụ đề xuất
				.ToListAsync();
            var viewModel = new ServiceDetailWithRecommendationsViewModel
            {
                Service = result,
                Recommendations = recommendations
            };
            return View(viewModel);
		}

	}
}
