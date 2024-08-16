using PhanThiHoaiAnh_223DATN_DVTC.Repository.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
    public class MenuModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập Tên thực đơn")]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required(ErrorMessage = "Nhập mô tả thực đơn")]
        public string Description { get; set; }
        public long Price { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; } = "noimage.jpg";
        public List<FoodModel> FoodItems { get; set; }
        public List<int> SelectedFoodItems { get; set; } // Danh sách ID món ăn được chọn
        public List<string> SelectedFoodItemNames { get; set; }// Danh sách tên món ăn được chọn

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }

	}

}
