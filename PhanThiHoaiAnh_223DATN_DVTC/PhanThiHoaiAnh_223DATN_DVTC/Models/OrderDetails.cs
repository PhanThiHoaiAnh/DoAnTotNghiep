using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class OrderDetails
	{
		public int Id { get; set; }
		[MaxLength(256)]
		public string UserName { get; set; }
		public string OrderCode { get; set; }
		public int ServiceId { get; set; }
		public long Price { get; set; }
		public int Quantity { get; set; }
		public DateTime ReceivedDate { get; set; }
		public float Discount { get; set; } = 0;
		public OtherServicesModel Service { get; set; }
		public OrderModel OrderModel { get; set; }
	}
}
