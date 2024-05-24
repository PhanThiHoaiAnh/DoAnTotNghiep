using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models.Views
{
	public class LoginViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
		public string UserName { get; set; }

		[DataType(DataType.Password), Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
		public string Password { get; set; }
		public string ReturnUrl { get; set; }
	}
}
