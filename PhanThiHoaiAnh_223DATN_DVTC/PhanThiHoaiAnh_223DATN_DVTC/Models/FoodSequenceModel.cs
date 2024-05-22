using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
    public class FoodSequenceModel
    {
        [Key, MaxLength(10, ErrorMessage = "Yêu cầu nhập Mã thứ tự món")]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
    }
}
