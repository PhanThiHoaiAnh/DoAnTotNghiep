namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
    public class CheckoutPageModel
    {
        public IEnumerable<CartItemModel> CartItems { get; set; }
        public CheckoutModel CheckoutModel { get; set; }
    }
}
