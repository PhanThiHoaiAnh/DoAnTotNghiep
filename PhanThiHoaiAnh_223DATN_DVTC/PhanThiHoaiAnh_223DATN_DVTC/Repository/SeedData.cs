using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;

namespace PhanThiHoaiAnh_223DATN_DVTC.Repository
{
    public class SeedData
    {
        public static void SeedingData(DataContext _context)
        {
            _context.Database.Migrate();
            if(!_context.OtherServices.Any())
            {
                ServiceCategoryModel chupanhs1 = new ServiceCategoryModel { Id="CACS", CategoryName = "Chụp ảnh cưới studio",Slug="chup anh cuoi",Description="Chụp ảnh cưới tại studio"};
                ServiceCategoryModel chupanhn2 = new ServiceCategoryModel { Id = "CACN", CategoryName = "Chụp ảnh cưới ngoại cảnh", Slug = "chup anh cuoi", Description = "Chụp ảnh cưới ở ngoại cảnh" };
                ServiceCategoryModel thiepmoi2 = new ServiceCategoryModel { Id = "TMSN", CategoryName = "Thiệp Sinh nhật", Slug = "thiep sinh nhat", Description = "Thiệp mời cho tiệc Sinh nhật" };
                _context.OtherServices.AddRange(
                    new OtherServicesModel { Name = "Thiệp Sinh nhật Hoạt hình", Slug = "Thiệp sinh nhật", Image = "1.jpg", Price = 3500, Description = "Thiệp Sinh nhật Hoạt hình đáng yêu cho các bé", Status = "Đang kinh doanh", Category = thiepmoi2 },
                    new OtherServicesModel { Name = "Chụp ảnh cưới đẹp với Studio", Slug = "Chụp ảnh cưới", Image = "2.jpg", Price = 3900000, Description = "Cô dâu : 2 soiree,chú rể : 2 vestPhoto + Makeup (trang điểm và làm tóc theo từng trang phục) + phụ kiện + hoa cầm tay", Status = "Đang kinh doanh", Category = chupanhs1 }
                 );
                _context.SaveChanges();
            }
            if (!_context.FoodModel.Any())
            {
                FoodSequenceModel foods1 = new FoodSequenceModel { Id = "MC", Name = "Món chính", Slug = "mon-chinh", Description = "Món chính trong tiệc" };
                FoodSequenceModel foods2 = new FoodSequenceModel { Id = "MTM", Name = "Món tráng miệng", Slug = "mon-trang-mieng", Description = "Món tráng miệng trong tiệc" };
                FoodCategoryModel foodc1 = new FoodCategoryModel { Id = "MMan", CategoryName = "Món mặn", Description = "Món ăn mặn" };
                FoodCategoryModel foodc2 = new FoodCategoryModel { Id = "MChien", CategoryName = "Món chiên", Description = "Món chiên dùng dẫu/ mỡ" };
                _context.FoodModel.AddRange(
                    new FoodModel { Name = "Gà bó xôi", Slug = "Ga-bo-xoi", Image = "ga.jpg", Price = 150000, Description = "1con gà/phần", FoodSequence = foods1, FoodCategory = foodc1 },
                    new FoodModel { Name = "Trái cây và sữa chua", Slug = "Traicay", Image = "fruit.jpg", Price = 150000, Description = "sữa chua và trái cây", FoodSequence = foods2, FoodCategory = foodc2 }
                 );
                _context.SaveChanges();
            }

        }
    }
}
