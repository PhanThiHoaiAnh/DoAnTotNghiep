using Microsoft.AspNetCore.Mvc;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;
using System.Security.Claims;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;
        public OrderController(DataContext context)
        {
            _dataContext = context;
        }
        public IActionResult Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var listOrder = _dataContext.tblOrderDetails
                 .Where(item => item.UserName == userEmail)
                 .ToList();
			var menuIds = listOrder.Select(item => item.ServiceId).Distinct().ToList();
			var menuNames = _dataContext.tblOtherServices
				 .Where(m => menuIds.Contains(m.Id))
				 .ToDictionary(m => m.Id, m => m.Name);

			ViewData["TenDichVu"] = menuNames;
			return View(listOrder);
        }
        public IActionResult IndexOrder()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var listOrder = _dataContext.tblOrder
                 .Where(item => item.UserName == userEmail)
                 .ToList();
            return View(listOrder);
        }

        public IActionResult IndexParty()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var listOrder = (from item in _dataContext.tblParty
                             where item.UserName == userEmail
                             select item).ToList();
            return View(listOrder);
        }
    }
}
