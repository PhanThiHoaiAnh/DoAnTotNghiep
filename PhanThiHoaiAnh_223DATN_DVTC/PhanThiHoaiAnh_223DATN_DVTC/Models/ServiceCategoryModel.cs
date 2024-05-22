using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
    public class ServiceCategoryModel
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập Tên loại dịch vụ")]
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập mô tả")]
        public string Description { get; set; }
    }
}
