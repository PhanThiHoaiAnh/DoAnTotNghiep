using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
	{
		private readonly DataContext _dataContext;
		public OrderController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		public async Task<IActionResult> Index()
		{
			return View(await _dataContext.Orders.OrderByDescending(o => o.Id).ToListAsync());
		}

        public async Task<IActionResult> ViewOrder(string ordercode)
        {
			var DetailsOrder = await _dataContext.OrderDetails.Include(od => od.Service).Where(od => od.OrderCode==ordercode).ToListAsync();
            return View(DetailsOrder);
        }
    }
}
