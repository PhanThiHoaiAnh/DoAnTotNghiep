namespace PhanThiHoaiAnh_223DATN_DVTC.Models.Views
{
	public class ServiceViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public long Price { get; set; }
		public string Category { get; set; }
		public string Image { get; set; }
	}
	public class ServiceDetailViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public long Price { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public string? Comment { get; set; }
		public float? Record {  get; set; } 
	}
	public class FoodDetailViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public long Price { get; set; }
		public string Category { get; set; }
		public string Sequence { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public string? Comment { get; set; }
		public float? Record { get; set; }
	}
	public class MenuDetailViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public long Price { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public string? Comment { get; set; }
		public float? Record { get; set; }
	}
	public class CardDetailViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public long Price { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public string? Comment { get; set; }
		public float? Record { get; set; }
	}
}
