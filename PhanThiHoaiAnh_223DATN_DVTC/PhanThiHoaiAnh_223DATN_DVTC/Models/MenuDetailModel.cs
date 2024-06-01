using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
    public class MenuDetailModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Chọn mã thực đơn")]
        public string MenuId { get; set; }
        [Required(ErrorMessage = "Chọn mã món")]
        public string FoodId { get; set; }
        public string Description { get; set; }
        public MenuModel Menu { get; set; }
        public FoodModel Food { get; set; }
    }
}
