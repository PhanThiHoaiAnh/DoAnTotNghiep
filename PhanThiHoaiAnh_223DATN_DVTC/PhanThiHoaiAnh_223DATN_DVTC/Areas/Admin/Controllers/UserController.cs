using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhanThiHoaiAnh_223DATN_DVTC.Models;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("Admin/AspNetRole")]
    [Authorize(Roles = "Quản lý")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<AppUserModel> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
