using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using PhanThiHoaiAnh_223DATN_DVTC.Helper;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Models.Views;

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
						//Kiểm tra quyền của người dùng và gán đường dẫn tới Admin nếu là Admin
						if (await _userManage.IsInRoleAsync(user, "Admin"))
						{
							return Redirect("/Admin"); // Đường dẫn tới trang Admin
						}

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
		public async Task<IActionResult> Profile()
		{
			var user = await _userManage.GetUserAsync(User);
			if (user == null)
			{
				// Xử lý khi không tìm thấy người dùng
				return RedirectToAction("Error", "Home");
			}
			var model = new List<ProfileViewModel>
			{
				new ProfileViewModel
				{
					FirstName = user.FName,
					LastName = user.LName,
					Birthday = user.Birthday,
					Email = user.Email,
					Gender = user.Gender,
					PhoneNumber = user.PhoneNumber,
					Address = user.Address
				}
				
			};
			return View(model);
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
					await _emailSender.SendEmailAsync(newUser.Email,"Xác nhận địa chỉ email", $"Vui lòng xác nhận địa chỉ email của bạn bằng cách nhấp vào đây {callbacklUrl}");
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
                return RedirectToAction("Error", "Home");
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
                return RedirectToAction("Error", "Home");
            }
        }
		[HttpGet]
		public async Task<IActionResult> EditProfile()
		{
			var user = await _userManage.GetUserAsync(User);
			if (user == null)
			{
				// Xử lý khi không tìm thấy người dùng
				return RedirectToAction("Error", "Home");
			}

			var model = new ProfileViewModel
			{
				FirstName = user.FName,
				LastName = user.LName,
				Birthday = user.Birthday,
				Email = user.Email,
				Gender = user.Gender,
				PhoneNumber = user.PhoneNumber,
				Address = user.Address,
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> EditProfile(ProfileViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Lấy người dùng hiện tại từ cơ sở dữ liệu
				var user = await _userManage.GetUserAsync(User);
				if (user == null)
				{
					// Xử lý khi không tìm thấy người dùng
					return RedirectToAction("Error", "Home");
				}

				// Cập nhật thông tin cá nhân từ model vào đối tượng người dùng
				user.FName = model.FirstName;
				user.LName = model.LastName;
				user.Birthday = model.Birthday;
				user.Email = model.Email;
				user.Gender = model.Gender;
				user.PhoneNumber = model.PhoneNumber;
				user.Address = model.Address;

				// Lưu các thay đổi vào cơ sở dữ liệu
				var result = await _userManage.UpdateAsync(user);
				if (result.Succeeded)
				{
					// Xử lý khi cập nhật thành công
					TempData["success"] = "Cập nhật thông tin thành công";
					return RedirectToAction("Profile");
				}
				else
				{
					// Xử lý khi cập nhật không thành công
					ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật thông tin cá nhân.");
				}
			}

			// Nếu ModelState không hợp lệ, hiển thị lại form chỉnh sửa với các lỗi
			return View(model);
		}
	}
}
