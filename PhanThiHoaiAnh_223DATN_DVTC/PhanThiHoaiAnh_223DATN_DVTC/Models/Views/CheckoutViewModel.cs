using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models.Views
{
    public class CheckoutViewModel
    {
        public string FName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty ;
        public string Address { get; set; } = string.Empty;
        [MaxLength(10)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage ="Vui lòng nhập ngày nhận dịch vụ")]
        public DateTime OrgDate { get; set; }
        public string Note { get; set; }
        public string TypePayment { get; set; }
    }
}
