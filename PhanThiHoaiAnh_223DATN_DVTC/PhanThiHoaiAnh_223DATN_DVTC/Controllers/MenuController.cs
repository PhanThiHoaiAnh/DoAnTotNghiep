using Microsoft.AspNetCore.Mvc;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
    public class MenuController : Controller
    {
        private readonly DataContext _dataContext;
        public MenuController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var menu = _dataContext.Menus.ToList();

            return View(menu);
        }
        public async Task<IActionResult> Detail(int Id)
        {
            if (Id == null) return RedirectToAction("Index");
            var serviceById = _dataContext.Menus.Where(s => s.Id == Id).FirstOrDefault();

            return View(serviceById);
        }
    }
}
