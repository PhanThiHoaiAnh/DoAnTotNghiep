namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class TaskModel
	{
		public int Id { get; set; }
		public int idNhanVien { get; set; }
		public string Name { get; set; }
		public int Party {  get; set; }
		public string Address { get; set; }
		public string Description { get; set; }
		public EmployeeModel NhanVien { get; set; }
		public PartyModel Tiec {  get; set; }
	}
}
