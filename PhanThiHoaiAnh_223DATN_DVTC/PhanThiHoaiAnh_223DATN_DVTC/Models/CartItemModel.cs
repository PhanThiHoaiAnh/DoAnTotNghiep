namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class CartItemModel
	{
		public long ServiceId { get; set; }
		public string ServiceName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal Total {
			get { return Quantity*Price; }
		}
        public string Image {  get; set; }
		public CartItemModel() 
		{

		}
		
		public CartItemModel(OtherServicesModel service)
		{
			ServiceId = service.Id;
            ServiceName = service.Name;
			Price = service.Price;
			Quantity = 1;
			Image = service.Image;
		}
	}
}
