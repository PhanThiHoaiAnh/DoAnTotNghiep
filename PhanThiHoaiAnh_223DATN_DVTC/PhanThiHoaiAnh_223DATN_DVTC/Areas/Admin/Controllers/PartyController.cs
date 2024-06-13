using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PartyController : Controller
    {
        private readonly DataContext _dataContext;
        public PartyController(DataContext context)
        {
            _dataContext = context;

        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Party.OrderByDescending(s => s.Id).ToListAsync());//Include(s => s.ThucDon)
        }
    }
}
