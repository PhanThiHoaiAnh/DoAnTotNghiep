using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class PartyCategoryModel
	{
		[Key]
		public string Id { get; set; }
		[Required(ErrorMessage = "Nhập Tên loại tiệc")]
		public string Name { get; set; }
		public string Description { get; set; }
		public string Slug { get; set; }
	}
}
