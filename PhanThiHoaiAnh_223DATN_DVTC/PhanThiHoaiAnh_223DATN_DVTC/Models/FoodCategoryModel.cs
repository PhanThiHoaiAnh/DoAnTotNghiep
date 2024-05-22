using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
    public class FoodCategoryModel
    {
        [Key, MaxLength(10, ErrorMessage = "Yêu cầu nhập Mã thứ tự món")]
        public string Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string Slug { get; set; }
    }
}
