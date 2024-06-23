using PhanThiHoaiAnh_223DATN_DVTC.Models.Views;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
    public class CheckoutPageModel
    {
        public IEnumerable<CartItemModel> CartItems { get; set; }
        public CheckoutViewModel CheckoutModel { get; set; }
    }
}
