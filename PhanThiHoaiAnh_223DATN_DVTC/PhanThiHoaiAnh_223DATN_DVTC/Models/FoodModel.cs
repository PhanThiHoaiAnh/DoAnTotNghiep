using PhanThiHoaiAnh_223DATN_DVTC.Repository.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class FoodModel
	{
        [Key]
        public string Id { get; set; }
		[Required(ErrorMessage = "Yêu cầu nhập Tên món ăn")]
		public string Name { get; set; }
		public string Slug { get; set; }
		[Required(ErrorMessage = "Nhập Giá món")]
		public decimal Price { get; set; }
		[Required(ErrorMessage = "Nhập mô tả món ăn")]
		public string Description { get; set; }
		public string FoodSequenceId { get; set; }
		public string FoodCategoryId { get; set; }
		public string Image {  get; set; }
		public FoodSequenceModel FoodSequence { get; set; }
		public FoodCategoryModel FoodCategory { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }

    }
}
