using System.ComponentModel.DataAnnotations.Schema;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class OrderDetails
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string OrderCode { get; set; }
		public long ServiceId { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public DateTime ReceivedDate { get; set; }
		public decimal Discount { get; set; } = 0;
		public OtherServicesModel Service { get; set; }
		public OrderModel OrderModel { get; set; }
	}
}
