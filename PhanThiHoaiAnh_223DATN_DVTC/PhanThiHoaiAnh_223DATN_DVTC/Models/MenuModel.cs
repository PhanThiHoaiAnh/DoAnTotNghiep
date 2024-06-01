using PhanThiHoaiAnh_223DATN_DVTC.Repository.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
    public class MenuModel
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập Tên thực đơn")]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required(ErrorMessage = "Nhập số lượng món trong thực đơn")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Nhập giá của thực đơn")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Nhập mô tả thực đơn")]
        public string Description { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}
