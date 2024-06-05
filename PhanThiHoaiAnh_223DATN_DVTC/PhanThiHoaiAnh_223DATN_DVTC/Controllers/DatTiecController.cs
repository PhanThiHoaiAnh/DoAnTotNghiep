using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
    public class DatTiecController : Controller
    {
        private readonly DataContext _dataContext;
		public DatTiecController(DataContext _context)
		{
			_dataContext = _context;
		}
		public IActionResult Index()
        {
			ViewBag.PartyCategories = new SelectList(_dataContext.PartyCategories, "Id", "Name");
			ViewBag.TableCategories = new SelectList(_dataContext.TableCategories, "Id", "Name");
			ViewBag.Location = new SelectList(_dataContext.Location, "Id", "Name");
			ViewBag.Menus = _dataContext.Menus.ToList();
			return View();
		}
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(PartyModel party)
		{
			

			//await _dataContext.SaveChangesAsync();
			return View(party);
		}
	}
}
