using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;
using PhanThiHoaiAnh_223DATN_DVTC.Services;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StatisticsController : Controller
    {
        private readonly DataContext _dataContext;
		public StatisticsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult GetMonthlyRevenue()
        {
            var monthlyRevenue = _dataContext.tblOrder
            .GroupBy(o => o.OrderOrg.Month)
            .Select(g => new MonthlyRevenueModel
            {
                Month = g.Key,
                Revenue = g.Sum(o => o.Total)
            })
            .ToList();
            return View(monthlyRevenue);
		}
        public IActionResult GetYearlyRevenue()
        {
            var yearlyRevenue = _dataContext.tblOrder
            .GroupBy(o => o.OrderOrg.Year)
            .Select(g => new YearlyRevenueModel
            {
                Year = g.Key,
                Revenue = g.Sum(o => o.Total)
            })
            .ToList();
            return View(yearlyRevenue);
        }
        public IActionResult GetOrderStatusStatistics()
        {
            var orderStatusStatistics = _dataContext.tblOrder
                .GroupBy(o => o.Status)
                .Select(g => new OrderStatusStatisticsModel
                {
                    Status = g.Key.ToString(),
                    Count = g.Count()
                })
                .ToList();
            return View(orderStatusStatistics);
        }
        public IActionResult GetMonthlyOrderCount()
        {
            var monthlyOrderCount = _dataContext.tblOrder
                .GroupBy(o => o.OrderOrg.Month)
                .Select(g => new MonthlyOrderCountModel
                {
                    Month = g.Key,
                    Count = g.Count()
                })
                .ToList();
            return View(monthlyOrderCount);
        }

    }
}
