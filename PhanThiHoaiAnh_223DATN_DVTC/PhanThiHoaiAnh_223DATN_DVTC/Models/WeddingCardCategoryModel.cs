using PhanThiHoaiAnh_223DATN_DVTC.Repository.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
    public class WeddingCardCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public long Price { get; set; }
        public string Image { get; set; } = "noimage.jpg";
        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}
