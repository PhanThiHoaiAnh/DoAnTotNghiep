using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
	[Area("Admin")]
    //[Route("Admin/AspNetRole")]
    [Authorize(Roles = "Admin")]
    public class AppRoleController : Controller
	{
        private readonly DataContext _dataContext;
        public AppRoleController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        //[Route("Index")]
        public IActionResult Index()
		{
			var roles = _dataContext.Roles.ToList();
			return View(roles);
		}
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(string id)
        {
            var role = _dataContext.Roles.FindAsync(id);
            return View(role);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var roleStore = new RoleStore<IdentityRole>(_dataContext);
                var roleValidators = new List<IRoleValidator<IdentityRole>>();
                var keyNormalizer = new UpperInvariantLookupNormalizer();
                var errors = new IdentityErrorDescriber();
                var logger = new Logger<RoleManager<IdentityRole>>(new LoggerFactory());

                var roleManager = new RoleManager<IdentityRole>(roleStore, roleValidators, keyNormalizer, errors, logger);
                roleManager.CreateAsync(model);
            }
            return Redirect("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IdentityRole model)
        {
			if (ModelState.IsValid)
				{
					var roleStore = new RoleStore<IdentityRole>(_dataContext);
					var roleValidators = new List<IRoleValidator<IdentityRole>>();
					var keyNormalizer = new UpperInvariantLookupNormalizer();
					var errors = new IdentityErrorDescriber();
					var logger = new Logger<RoleManager<IdentityRole>>(new LoggerFactory());
					var roleManager = new RoleManager<IdentityRole>(roleStore, roleValidators, keyNormalizer, errors, logger);
				var role = await roleManager.FindByIdAsync(model.Id);
				if (role != null)
				{
					role.Id = model.Id;
					role.Name = model.Name;
					await roleManager.UpdateAsync(role);
				}
			}
			return RedirectToAction("Index");
        }
		public async Task<IActionResult> Delete(string id)
		{
			var roleManager = HttpContext.RequestServices.GetService<RoleManager<IdentityRole>>();

			var role = await roleManager.FindByIdAsync(id);
			if (role != null)
			{
				var result = await roleManager.DeleteAsync(role);
				if (result.Succeeded)
				{
                    TempData["success"] = "Xóa thành công";
				}
			}
			else
			{
				// Quyền không tồn tại
				TempData["success"] = "Quyền không tồn tại";
			}

			return RedirectToAction("Index");
		}
	}
}
