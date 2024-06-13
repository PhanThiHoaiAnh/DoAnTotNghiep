using PhanThiHoaiAnh_223DATN_DVTC.Repository.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
    public class OtherServicesModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập Tên dịch vụ")]
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; } = "noimage.jpg";
        [Required(ErrorMessage = "Yêu cầu nhập Giá dịch vụ")]
        public long Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage = "Chọn loại dịch vụ")]
        public string CategoryId { get; set; }
        public ServiceCategoryModel Category { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
        
    }
}
