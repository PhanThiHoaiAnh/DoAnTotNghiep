using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class PartyModel
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Chọn ngày và giờ cụ thể")]
		public DateTime OrgDate { get; set; }
		public string Time {  get; set; }
		[Required(ErrorMessage = "Nhập số lượng khách")]
		public int CusNumber { get; set; }
		[Required(ErrorMessage = "Chọn loại tiệc")]
		public string PartyCategoryId { get; set; }
		[Required(ErrorMessage = "Chọn loại địa điểm tiệc")]
		public string LocationId { get; set; }
		[Required(ErrorMessage = "Nhập chi tiết địa điểm tiệc")]
		public string LocationName { get; set; }
		[Required(ErrorMessage = "Chọn loại bàn tiệc")]
		public int Table { get; set; }
		public PartyCategoryModel PtCategory { get; set; }
		public LocationModel LocationPt { get; set; }
		public TableCategoryModel TablePt { get; set; }

	}
}
