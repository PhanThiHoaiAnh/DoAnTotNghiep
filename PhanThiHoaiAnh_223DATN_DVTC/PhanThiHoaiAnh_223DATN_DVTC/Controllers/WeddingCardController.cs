using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models.Views;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
	public class WeddingCardController : Controller
	{
        private readonly DataContext _dataContext;
        public WeddingCardController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IActionResult Index()
		{
            var card = _dataContext.tblWeddingCardCategories.ToList();
			return View(card);
		}
		public IActionResult Details(int Id)
		{
			var card = _dataContext.tblWeddingCardCategories.SingleOrDefault(p => p.Id == Id);
			if (card == null) 
			{ 
				return RedirectToAction("Index");
			}
			
			var result = new CardDetailViewModel
			{
				Id = card.Id,
				Name = card.Name,
				Price = card.Price,
				Image = card.Image,
				Description = card.Description,
			};
			return View(result);
		}
	}
}
