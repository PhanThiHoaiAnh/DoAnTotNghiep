using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class TableCategoryModel
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required(ErrorMessage = "Nhập số lượng khách/bàn")]
		public int Quantity { get; set; }
		public string Description { get; set; }
	}
}
