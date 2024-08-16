using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models.Views
{
	public class ForgotPasswordViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập Email")]
		public string Email { get; set; }
	}
}
