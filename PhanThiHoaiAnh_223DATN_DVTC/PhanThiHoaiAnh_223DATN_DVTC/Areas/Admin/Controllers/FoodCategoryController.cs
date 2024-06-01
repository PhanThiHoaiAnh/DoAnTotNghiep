﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class FoodCategoryController : Controller
    {
        private readonly DataContext _dataContext;
        public FoodCategoryController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.FoodCategories.OrderByDescending(s => s.Id).ToListAsync());
        }
    }
}
