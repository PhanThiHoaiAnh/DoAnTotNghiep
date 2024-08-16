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

        public DbSet<ServiceCategoryModel> tblServiceCategories { get; set; }
        public DbSet<OtherServicesModel> tblOtherServices { get; set; }
		public DbSet<OrderDetails> tblOrderDetails { get; set; }
        public DbSet<FoodModel> tblFood { get; set; }
        public DbSet<FoodCategoryModel> tblFoodCategories { get; set; }
        public DbSet<FoodSequenceModel> tblFoodSequence { get; set; }
        public DbSet<MenuModel> tblMenu { get; set; }
        public DbSet<MenuDetail> tblMenuDetails { get; set; }
        public DbSet<PartyCategoryModel> tblPartyCategories { get; set; }
		public DbSet<PartyModel> tblParty { get; set; }
		public DbSet<LocationModel> tblLocation { get; set; }
        public DbSet<DatTiecModel> tblOrder { get; set; }
		public DbSet<PositionModel> tblPositions { get; set; }
        public DbSet<WeddingCardModel> tblOrderWeddingCard { get; set;}
        public DbSet<WeddingCardCategoryModel> tblWeddingCardCategories { get; set;}
        public DbSet<EmployeeModel> tblMembers { get; set; }
		public DbSet<TaskModel> tblTasks { get; set; }

	}
}
