using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
    public class OrderRepository
    {
        private readonly DataContext _dataContext;

        public OrderRepository(DataContext context)
        {
            _dataContext = context;
        }

        public List<DatTiecModel> GetOrdersByMonth(int year, int month)
        {
            return _dataContext.tblOrder
                .Where(o => o.OrderOrg.Year == year && o.OrderOrg.Month == month)
                .ToList();
        }
    }
}
