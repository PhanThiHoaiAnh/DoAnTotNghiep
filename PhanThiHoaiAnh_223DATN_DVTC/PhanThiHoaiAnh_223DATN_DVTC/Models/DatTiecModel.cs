using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class DatTiecModel
	{
		public int Id { get; set; }
		public string PartyCode { get; set; }
		[MaxLength(256)]
		public string UserName { get; set; }
		public int MenuId { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime OrderOrg { get; set; }
		public int PartyId { get; set; }
		public string PartyName { get; set;}
	}
}
