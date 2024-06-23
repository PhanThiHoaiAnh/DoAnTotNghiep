using System.Collections.Generic;
namespace PhanThiHoaiAnh_223DATN_DVTC.Models.Views
{
    public class ServiceDetailWithRecommendationsViewModel
    {
        public ServiceDetailViewModel Service { get; set; }
        public List<OtherServicesModel> Recommendations { get; set; }
    }
}
