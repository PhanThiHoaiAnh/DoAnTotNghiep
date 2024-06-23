using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PhanThiHoaiAnh_223DATN_DVTC.Repository.Components
{
    public class FoodCategoriesViewComponent : ViewComponent
    {
        private readonly DataContext _dataContext;  
        public FoodCategoriesViewComponent(DataContext context)
        {
            _dataContext = context;
        }
		public async Task<IViewComponentResult> InvokeAsync() => View(await _dataContext.tblFoodCategories.ToListAsync());


    }
}
