using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Models.Views;
using PhanThiHoaiAnh_223DATN_DVTC.Services;
using System.Text.Encodings.Web;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
    public class AccountController : Controller
	{
		private UserManager<AppUserModel> _userManage;
		private SignInManager<AppUserModel> _signInManager;
		private readonly IEmailSender _emailSender;
		public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManage, IEmailSender emailSender) 
		{
			_signInManager = signInManager;
			_userManage = userManage;
			_emailSender = emailSender;

        }
		public IActionResult Login(string returnUrl)
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl});
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVM)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManage.FindByEmailAsync(loginVM.UserName);
				if (user != null)
				{
					if (!await _userManage.IsEmailConfirmedAsync(user))
					{
                        ModelState.AddModelError("", "Tài khoản chưa được xác nhận email. Vui lòng kiểm tra email và xác nhận tài khoản trước khi đăng nhập.");
                        return View(loginVM);
                    }
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);
					if (result.Succeeded)
					{
						return Redirect(loginVM.ReturnUrl ?? "/");
					}
					
                }
				ModelState.AddModelError("", "Tên đăng nhập hoặc Mật khẩu không đúng");
			}
			return View(loginVM);
		}
		public IActionResult Create()
		{
			return View();
		}
       
        [HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(UserModel user)
		{
			if (ModelState.IsValid)
			{
                AppUserModel newUser = new AppUserModel { LName = user.LastName, FName = user.FirstName, UserName = user.Email, Email = user.Email, Birthday = user.Birth, Gender = user.Gender, PhoneNumber = user.PhoneNumber, Address = user.Address, EmailConfirmed = false };
                IdentityResult result = await _userManage.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    string code = await _userManage.GenerateEmailConfirmationTokenAsync(newUser);
                    var callbacklUrl = Url.Action("ConfirmEmail", "Account", new { userId = newUser.Id, code = code }, Request.Scheme);
					await _emailSender.SendEmailAsync(newUser.Email,"Xác nhận địa chỉ email", $"Vui lòng xác nhận địa chỉ email của bạn bằng cách nhấp vào <a href='{callbacklUrl}'>đây</a>.");
                    return View("NotificationEmailConfirm");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
			return View(user);
		}
		public async Task<IActionResult> Logout(string returnUrl="/")
		{
			await _signInManager.SignOutAsync();
			return Redirect(returnUrl);
		}

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                // Xử lý khi thiếu thông tin userId hoặc code
                return RedirectToAction("Error", "Home"); // Hoặc chuyển hướng tới trang lỗi khác
            }

            var user = await _userManage.FindByIdAsync(userId);
            if (user == null)
            {
                // Xử lý khi không tìm thấy người dùng
                return RedirectToAction("Error", "Home"); // Hoặc chuyển hướng tới trang lỗi khác
            }

            var result = await _userManage.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                // Xác nhận email thành công
                return View("ConfirmEmail");
            }
            else
            {
                // Xử lý khi xác nhận email không thành công
                return RedirectToAction("Error", "Home"); // Hoặc chuyển hướng tới trang lỗi khác
            }
        }

    }
}
