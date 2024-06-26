﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PhanThiHoaiAnh_223DATN_DVTC.Repository.Components
{
    public class ServiceCategoriesViewComponent : ViewComponent
    {
		private readonly DataContext _dataContext;
		public ServiceCategoriesViewComponent(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IViewComponentResult> InvokeAsync() => View(await _dataContext.ServiceCategories.ToListAsync());

	}
}
