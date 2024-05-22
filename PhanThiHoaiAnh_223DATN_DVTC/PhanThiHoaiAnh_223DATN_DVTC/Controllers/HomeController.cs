using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;
using System.Diagnostics;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _dataContext = context;
        }
		public IActionResult Index()
        {
            var services = _dataContext.OtherServices.Include("Category").ToList();

            return View(services);
		}

		public IActionResult Index1()
		{
			var food = _dataContext.FoodModel.Include("FoodSequence").Include("FoodCategory").ToList();

			return View(food);
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
