using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AppUserRoleController : Controller
	{
		private readonly DataContext _dataContext;
		public AppUserRoleController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		//[Route("Index")]
		public IActionResult Index()
		{
			var roles = _dataContext.UserRoles.ToList();
			return View(roles);
		}
		public IActionResult Create()
		{
			
			ViewBag.Users = new SelectList(_dataContext.Users.ToList(), "Id", "UserName");
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> Create(string userId, string roleId)
        {

            var roles = _dataContext.Roles.ToList();
            var users = _dataContext.Users.ToList();

            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            ViewBag.Users = new SelectList(users, "Id", "UserName");

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(roleId))
            {
                var userRole = new IdentityUserRole<string>
                {
                    UserId = userId,
                    RoleId = roleId
                };

                _dataContext.UserRoles.Add(userRole);
                await _dataContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
