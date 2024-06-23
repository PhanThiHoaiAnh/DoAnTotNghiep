using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models.Views
{
    public class PartyViewModel
    {
        public string PartyCode { get; set; }
        [MaxLength(256)]
        public string UserName { get; set; }
        public string ServiceName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderOrg { get; set; }
        public string Address { get; set; }
        public int Quantity { get; set; }
        public long Total { get; set; }
        public long Deposit { get; set; }
        public long Pay { get; set; }
       
    }
}
