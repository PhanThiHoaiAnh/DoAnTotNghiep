using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
    public class WeddingCardModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BrideName { get; set; }
        [Required]
        public string GroomName { get; set; }
		[Required]
		public string BrideMoName { get; set; }
		[Required]
		public string BrideFaName { get; set; }
		[Required]
		public string BrideAddress { get; set; }
		[Required]
		public string GroomFaName { get; set; }
		[Required]
		public string GroomMoName { get; set; }
		[Required]
		public string GroomAddress { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string TimeWedding { get; set; }
        [Required]
        public string AddressWedding { get; set; }
        [Required]
        public DateTime DateWedding { get; set; }
        public int CardId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateTime ReceiveDate { get; set; }
		[Required, MaxLength(10)]
		public string PhoneNumber { get; set; }
		[Required]
		public string AddressReceived { get; set; }
		public long Total { get; set; }
        public bool Status { get; set; }
        public string OrderCode { get; set; }
		public WeddingCardCategoryModel CardModel { get; set; }
        public string Note { get; set; }
        [MaxLength(256)]
        public string UserName { get; set; }
        public UserModel User { get; set; }
    }
}
