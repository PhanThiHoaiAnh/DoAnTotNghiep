using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;

namespace PhanThiHoaiAnh_223DATN_DVTC.Repository
{
    public class DataContext :IdentityDbContext<AppUserModel>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
            
        }

        public DbSet<ServiceCategoryModel> ServiceCategories { get; set; }
        public DbSet<OtherServicesModel> OtherServices { get; set; }
        public DbSet<FoodCategoryModel> FoodCategories { get; set; }
        public DbSet<FoodSequenceModel> FoodSequence { get; set; }
        public DbSet<FoodModel> FoodModel { get; set; }
		public DbSet<OrderModel> Orders { get; set; }
		public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
