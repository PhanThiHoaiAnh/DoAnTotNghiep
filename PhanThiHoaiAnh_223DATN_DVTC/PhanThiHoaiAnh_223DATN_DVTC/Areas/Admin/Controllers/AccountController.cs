using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Models.Views;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private UserManager<AppUserModel> _userManage;
        private SignInManager<AppUserModel> _signInManager;
        private readonly DataContext _dataContext;
        public AccountController(DataContext context, UserManager<AppUserModel> userManage, SignInManager<AppUserModel> signInManager)
        {
            _dataContext = context;
            _userManage = userManage;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Users.ToListAsync());
            
        }
		public async Task<IActionResult> EmployeeIndex()
		{
			return View(await _dataContext.tblMembers.ToListAsync());

		}
		public IActionResult Create()
        {
			ViewBag.Roles = new SelectList(_dataContext.Roles.ToList(), "Id", "Name");
			ViewBag.Positions = new SelectList(_dataContext.tblPositions.ToList(), "Id", "Name");
			return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeModel user)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(_dataContext.Roles.ToList(), "Id", "Name");
                ViewBag.Positions = new SelectList(_dataContext.tblPositions.ToList(), "Id", "Name");
                AppUserModel newUser = new AppUserModel 
                { 
                    LName = user.LastName, FName = user.FirstName, 
                    UserName = user.UserName, Email = user.Email, Birthday = user.Birthday,
                    Gender = user.Gender, PhoneNumber = user.PhoneNumber, Address = user.Address, 
                    EmailConfirmed = true, Position = user.Position, IRole = user.Rolee 
                };
                EmployeeModel newMember = new EmployeeModel
                {
                    FirstName = user.FirstName, LastName = user.LastName, UserName = user.FirstName,
                    Email = user.Email, Address = user.Address, Birthday=user.Birthday,
                    Gender = user.Gender, PhoneNumber = user.PhoneNumber,
					Position = user.Position, Rolee = user.Rolee, Password = user.Password
				};
                IdentityResult result = await _userManage.CreateAsync(newUser, user.Password);
                await _dataContext.tblMembers.AddAsync(newMember);
                
                if (result.Succeeded)
                {
                    IdentityUserRole<string> userRole = new IdentityUserRole<string>{
                        UserId = newUser.Id,
                        RoleId = user.Rolee
                    };
                    await _dataContext.UserRoles.AddAsync(userRole);
                    await _dataContext.SaveChangesAsync();
                    return RedirectToAction("Index","Account");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }
    }
}
