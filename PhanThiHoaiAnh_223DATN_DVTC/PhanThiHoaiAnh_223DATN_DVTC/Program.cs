using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

var builder = WebApplication.CreateBuilder(args);

//Connection Db
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectedDb"]);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.IsEssential = true;
});

builder.Services.AddIdentity<AppUserModel,IdentityRole>()
	.AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
	// Password settings.
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 5;

	options.User.RequireUniqueEmail = true;
});

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();//xac thuc
app.UseAuthorization();//quyen

//map BE
app.MapControllerRoute(
	name: "Areas",
	pattern: "{area:exists}/{controller=OtherServices}/{action=Index}/{id?}");

//map FE
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// code seeding data
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedingData(context);

app.Run();
