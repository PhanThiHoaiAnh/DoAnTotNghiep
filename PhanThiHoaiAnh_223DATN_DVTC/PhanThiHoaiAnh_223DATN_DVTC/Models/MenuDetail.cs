using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
    public class MenuDetail
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Chọn thực đơn")]
        public int MenuId { get; set; }
        [Required(ErrorMessage = "Chọn món")]
        public int FoodId { get; set; }
        public long Price { get; set; }
        public MenuModel Menu { get; set; }
        public FoodModel Food { get; set; }
    }
}
