﻿namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class OrderModel
	{
		public int Id { get; set; }
		public string OrderCode { get; set; }
		public string UserName { get; set; }
		public DateTime OrderDate { get; set; }
		public int Status { get; set; }
		public DateTime ReceivedDate { get; set; }

	}
}