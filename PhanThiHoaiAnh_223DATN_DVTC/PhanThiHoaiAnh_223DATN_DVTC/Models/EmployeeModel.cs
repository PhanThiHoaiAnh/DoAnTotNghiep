using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class EmployeeModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập tên nhân viên")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập họ")]
		public string LastName { get; set; }
		[MaxLength(256)]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập Email"), EmailAddress]
		public string Email { get; set; }
		[DataType(DataType.Password), Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
		public string Password { get; set; }
		public DateOnly Birthday { get; set; }
		public string Gender { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập số điện thoại"), MaxLength(10)]
		public string PhoneNumber { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
		public string Address { get; set; }
		public string Position { get; set; }
        public string Rolee { get; set; }
        public PositionModel ChucVu { get; set; }
		public IdentityRole IRole { get; set; }
	}
}
