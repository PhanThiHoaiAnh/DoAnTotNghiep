using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
    public class CheckoutModel
    {
        public bool GiongKhachHang {  get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? Address { get; set; }
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }
        public DateTime OrgDate { get; set; }
        public string? Note { get; set; }
    }
}
