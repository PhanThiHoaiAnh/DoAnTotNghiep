using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class HoaDonModel
	{
		public string Id { get; set; }
		public string FName { get; set; }
        public string LName { get; set; }
        public DateTime OrderDate { get; set; }
		public DateTime OrgDate { get; set;}
		public string Address {  get; set; }
		[MaxLength(10)]
		public string PhoneNum { get; set; }
		public string Payment { get; set; }
		public string Note { get; set; }
		public bool Status { get; set; }
		[MaxLength(256)]
		public string UserName { get; set; }
		public UserModel User { get; set; }
		public PartyModel Party { get; set; }

	}
}
