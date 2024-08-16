using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class DatTiecModel
	{
		public int Id { get; set; }
        public string PartyCode { get; set; }
        public string FtName { get; set; } = string.Empty;
		public string LtName { get; set; } = string.Empty;
        [MaxLength(256)]
		public string UserName { get; set; }
		public string ServiceName { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime OrderOrg { get; set; }
		public string Address { get; set; }
		public int Quantity { get; set; }
        public long Total {  get; set; }
		public long Deposit { get; set; }
		public long Pay { get; set; }
		public int Status { get; set; }
		public string Payment { get; set; }
		public string PhoneNumber { get; set; }
		public string Note { get; set; }
		public List<string> FoodList { get; set; }
        public List<string> ServiceList { get; set; }
        public PartyModel Tiec { get; set; }
        public UserModel User { get; set; }
        public MenuModel Menu { get; set; }
        public OrderDetails Detail { get; set; }
    }
}
