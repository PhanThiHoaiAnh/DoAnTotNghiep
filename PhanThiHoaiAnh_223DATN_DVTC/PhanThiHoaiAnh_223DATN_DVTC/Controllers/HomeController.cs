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
		public IActionResult Index(string name)
        {
            var services = _dataContext.OtherServices.Include("Category").ToList();
            return View(services);
		}
		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statuscode)
        {
            if (statuscode == 404)
            {
                return View("NotFound");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult TimKiem(string name)
        {
            var services = from s in _dataContext.OtherServices select s;
            if (!string.IsNullOrEmpty(name))
            {
                services = services.Where(s => s.Name.Contains(name));
            }
            return View(services);
        }
    }
}
