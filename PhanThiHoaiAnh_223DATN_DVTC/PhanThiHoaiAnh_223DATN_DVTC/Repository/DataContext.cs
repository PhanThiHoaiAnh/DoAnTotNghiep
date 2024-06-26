﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
		public DbSet<OrderModel> Orders { get; set; }
		public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<FoodModel> FoodModel { get; set; }
        public DbSet<FoodCategoryModel> FoodCategories { get; set; }
        public DbSet<FoodSequenceModel> FoodSequence { get; set; }
        public DbSet<MenuModel> Menus { get; set; }
        public DbSet<MenuDetail> MenuDetails { get; set; }
        public DbSet<PartyCategoryModel> PartyCategories { get; set; }
		public DbSet<PartyModel> Party { get; set; }
		public DbSet<LocationModel> Location { get; set; }
        public DbSet<DatTiecModel> OrderParty { get; set; }
		public DbSet<PositionModel> Positions { get; set; }
		public DbSet<HoaDonModel> Bills { get; set; }
        public DbSet<WeddingCardModel> OrderWeddingCard { get; set;}
        public DbSet<WeddingCardCategoryModel> WeddingCardCategories { get; set;}
        public DbSet<EmployeeModel> Members { get; set; }
		public DbSet<TaskModel> tblTasks { get; set; }
		
	}
}
