namespace PhanThiHoaiAnh_223DATN_DVTC.Services
{
    public interface IOrderRepository
    {
        long GetMonthlyRevenue();
        int GetTotalOrders();
        Dictionary<string, int> GetOrderStatusStatistics();
        Dictionary<int, int> GetMonthlyOrderCount();
    }
}
