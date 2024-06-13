using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class PartyModel
	{
		[Key]
		public int Id { get; set; }
		public string PartyCode { get; set; }
		[Required(ErrorMessage = "Chọn ngày và giờ cụ thể")]
		public DateTime OrgDate { get; set; }
		public string Time {  get; set; }
		[Required(ErrorMessage = "Nhập số lượng khách")]
		public int CusNumber { get; set; }
		[Required(ErrorMessage = "Chọn loại tiệc")]
		public string PartyCategoryId { get; set; }
		[Required(ErrorMessage = "Chọn loại địa điểm tiệc")]
		public int LocationId { get; set; }
		[Required(ErrorMessage = "Nhập chi tiết địa điểm tiệc")]
		public string LocationName { get; set; }
		[Required(ErrorMessage = "Số người trên bàn tiệc")]
        public int PersonTable { get; set; }

        [Required(ErrorMessage = "Chọn Thực đơn tiệc")]
		public int MenuParty { get; set; }
		public int NumTable { get; set; }//=> CusNumber/PersonTable;
		public long Total { get; set; }
        public string Note { get; set; }
        [Required(ErrorMessage = "Nhập số điện thoại liên hệ")]
        public string PhoneNumber { get; set; }
        [MaxLength(256)]
		public string UserName { get; set; }
        public long Deposit { get; set; }
        public long Pay {  get; set; }
        public int otherService { get; set; }
		//public List<int> SelectedService { get; set; }
		public int Status { get; set; }
        public DateTime OrderDate { get; set; }
        public PartyCategoryModel PtCategory { get; set; }
		public LocationModel LocationPt { get; set; }
		public MenuModel ThucDon { get; set; }
		public OtherServicesModel Service { get; set;}
	}
}
