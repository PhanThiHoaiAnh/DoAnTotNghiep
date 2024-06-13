namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class CartItemModel
	{
		public int ServiceId { get; set; }
		public string ServiceName { get; set; }
		public int Quantity { get; set; }
		public long Price {get; set;  }
		public long Total { get { return Price* Quantity; } }
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
		public CartItemModel(FoodModel food)
		{
			ServiceId = food.Id;
			ServiceName = food.Name;
			Price = food.Price;
			Quantity = 1;
			Image = food.Image;
		}
		public CartItemModel(MenuModel menu)
		{
			ServiceId = menu.Id;
			ServiceName = menu.Name;
			Price = menu.Price;
			Quantity = 1;
			Image = menu.Image;
		}
	}
}
