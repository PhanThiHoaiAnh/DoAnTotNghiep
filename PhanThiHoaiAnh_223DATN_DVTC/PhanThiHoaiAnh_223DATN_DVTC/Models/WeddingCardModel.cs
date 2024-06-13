using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
    public class WeddingCardModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string TenCoDau { get; set; }
		[Required]
		public string TenChuRe { get; set; }
		[Required]
		public string TenChaCoDau { get; set; }
		[Required]
		public string TenMeCoDau { get; set; }
		[Required]
		public string AddressCoDau { get; set; }
		[Required]
		public string TenChaChuRe { get; set; }
		[Required]
		public string TenMeChuRe { get; set; }
		[Required]
		public string AddressChuRe { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string TimeWediing { get; set; }
        [Required]
        public string AddrWedding { get; set; }
        public int CardId { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
		public WeddingCardCategoryModel CardModel { get; set; }
        public string Note { get; set; }
        [MaxLength(256)]
        public string UserName { get; set; }
        public UserModel User { get; set; }
    }
}
