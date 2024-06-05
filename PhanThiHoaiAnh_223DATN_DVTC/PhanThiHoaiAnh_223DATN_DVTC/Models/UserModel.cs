using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập tên của bạn")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập họ của bạn")]
		public string LastName { get; set; }
		[MaxLength(256)]
		public string UserName {  get; set; }
		[Required(ErrorMessage = "Vui lòng nhập Email"),EmailAddress]
		public string Email { get; set; }
		[DataType(DataType.Password),Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
		public string Password { get; set; }
		public DateOnly Birth {  get; set; }
		public string Gender { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập số điện thoại của bạn")]
		public string PhoneNumber { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập địa chỉ của bạn")]
		public string Address { get; set; }
	}
}
