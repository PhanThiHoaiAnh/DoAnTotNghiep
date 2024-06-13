using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Models.Views;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;
using System.Security.Claims;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
	public class CartController : Controller
	{
		private readonly DataContext _dataContext;
		public CartController(DataContext _context)
		{
			_dataContext = _context;
		}
		public List<CartItemModel> cart => HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
		public IActionResult Index()
		{
			return View(cart);
		}
		public IActionResult Success()
		{
			return View();
		}
		public async Task<IActionResult> Add(int Id, int quantity=1)
		{
			OtherServicesModel service = await _dataContext.OtherServices.FindAsync(Id);
			var gioHang = cart;
			var cartItems = gioHang.SingleOrDefault(s => s.ServiceId == Id);
			if (cartItems == null)
			{
				if(service == null)
				{
					TempData["Message"] = $"Không tìm thấy mã dịch vụ{Id}";
					return Redirect("/404");
				}
				cartItems = new CartItemModel
				{
					ServiceId = service.Id,
					ServiceName = service.Name,
					Price = service.Price,
					Quantity = quantity
				};
				gioHang.Add(cartItems);
			}
			else
			{
				cartItems.Quantity += quantity;
			}
			HttpContext.Session.SetJson("Cart", gioHang);
			TempData["success"] = "Thêm vào giỏ hàng thành công";
			return Redirect(Request.Headers["Referer"].ToString());
			
		}
		public async Task<IActionResult> Decrease(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");

			CartItemModel cartItem = cart.Where(c => c.ServiceId == Id).FirstOrDefault();

			if (cartItem.Quantity > 1)
			{
				--cartItem.Quantity;
			}
			else
			{
				cart.RemoveAll(s => s.ServiceId == Id);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}

			return RedirectToAction("Index");

		}
		public async Task<IActionResult> Increase(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");

			CartItemModel cartItem = cart.Where(c => c.ServiceId == Id).FirstOrDefault();

			if (cartItem.Quantity >= 1)
			{
				++cartItem.Quantity;
			}
			else
			{
				cart.RemoveAll(s => s.ServiceId == Id);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}

			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Remove(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");

			cart.RemoveAll(s => s.ServiceId == Id);

			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}

			TempData["success"] = "Đã xóa dịch vụ khỏi giỏ hàng thành công";
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Clear()
		{
			HttpContext.Session.Remove("Cart");
			TempData["success"] = "Đã xóa giỏ hàng thành công";
			return RedirectToAction("Index");
		}
		// Mon An
		public async Task<IActionResult> AddFood(int Id)
		{
			FoodModel food = await _dataContext.FoodModel.FindAsync(Id);

			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

			CartItemModel cartItems = cart.Where(s => s.ServiceId == Id).FirstOrDefault();
			if (cartItems == null)
			{
				cart.Add(new CartItemModel(food));
			}
			else
			{
				cartItems.Quantity += 1;
			}

			HttpContext.Session.SetJson("Cart", cart);
			TempData["success"] = "Thêm vào giỏ hàng thành công";

			return Redirect(Request.Headers["Referer"].ToString());
		}
		// Thuc Don
		public async Task<IActionResult> AddMenu(int Id)
		{
			MenuModel menu = await _dataContext.Menus.FindAsync(Id);

			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

			CartItemModel cartItems = cart.Where(s => s.ServiceId == Id).FirstOrDefault();
			if (cartItems == null)
			{
				cart.Add(new CartItemModel(menu));
			}
			else
			{
				cartItems.Quantity += 1;
			}

			HttpContext.Session.SetJson("Cart", cart);
			TempData["success"] = "Thêm vào giỏ hàng thành công";

			return Redirect(Request.Headers["Referer"].ToString());
		}
        //thanh toan

        [Authorize]
        [HttpGet]
        public ActionResult Checkout()
        {
            if(cart.Count == 0)
			{
				return Redirect("/");
			}	
            return View(cart);
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult Checkout(CheckoutModel model)
        {
            if (ModelState.IsValid)
            {
                //var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var userId = HttpContext.User.FindFirstValue("Id");

                var khachHang = new AppUserModel();
                if (model.GiongKhachHang == true)
                {
					khachHang = _dataContext.Users.SingleOrDefault(k => k.Id == userId);//Users.SingleOrDefault(k => k.UserName == userEmail);
                }
                var hoaDon = new HoaDonModel
                {
                    Id = Guid.NewGuid().ToString(),
                    FName = model.FName ?? khachHang.FName ,
                    LName = model.LName ?? khachHang.LName,
                    UserName = khachHang.UserName,
                    Address = model.Address ?? khachHang.Address,
                    PhoneNum = model.PhoneNumber ?? khachHang.PhoneNumber,
                    OrderDate = DateTime.Now,
                    OrgDate = model.OrgDate,
                    Payment = "COD",
                    Status = false,
                    Note = model.Note
                };
                _dataContext.Database.BeginTransaction();
                try
				{
					_dataContext.Database.CommitTransaction();
                    _dataContext.Add(hoaDon);
                    _dataContext.SaveChanges();
					var cthd = new List<OrderDetails>();
					foreach( var item in cart)
					{
						cthd.Add(new OrderDetails
						{
                            OrderCode = hoaDon.Id,
							UserName = hoaDon.UserName,
                            ServiceId= item.ServiceId,
							Price = item.Price,
							Quantity = item.Quantity,
							ReceivedDate = hoaDon.OrgDate,
							Discount = 0
                        });
					}
					_dataContext.AddRange(cthd);
					_dataContext.SaveChanges();

                    HttpContext.Session.Remove("Cart");
                    return RedirectToAction("Success", "Cart");
                }
				catch
				{
					_dataContext.Database.RollbackTransaction();
				}
			}
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult CheckoutPage(CheckoutPageModel model)
		{
            if (ModelState.IsValid)
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                if (model.CheckoutModel.GiongKhachHang == true)
                {
                    var khach = _dataContext.Users.SingleOrDefault(k => k.UserName == userEmail);
                }
                var khachHang = new UserModel();
                var hoaDon = new HoaDonModel
                {
                    Id = Guid.NewGuid().ToString(),
                    FName = model.CheckoutModel.FName ?? khachHang.FirstName,
                    LName = model.CheckoutModel.LName ?? khachHang.LastName,
                    UserName = userEmail,
                    Address = model.CheckoutModel.Address ?? khachHang.Address,
                    PhoneNum = model.CheckoutModel.PhoneNumber ?? khachHang.PhoneNumber,
                    OrderDate = DateTime.Now,
                    OrgDate = model.CheckoutModel.OrgDate,
                    Payment = "COD",
                    Status = false,
                    Note = model.CheckoutModel.Note,
                };
                _dataContext.Database.BeginTransaction();
                try
                {
                    _dataContext.Database.CommitTransaction();
                    _dataContext.Add(hoaDon);
                    _dataContext.SaveChanges();
                    var odDetail = new List<OrderDetails>();
                    foreach (var item in cart)
                    {
                        odDetail.Add(new OrderDetails
                        {
                            OrderCode = hoaDon.Id,
                            ServiceId = item.ServiceId,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            Discount = 0,
                            ReceivedDate = hoaDon.OrgDate
                        });
                    }
                    _dataContext.SaveChanges();
                    HttpContext.Session.Remove("Cart");
                    TempData["success"] = "Thanh toán thành công. Vui lòng đợi duyệt!";
                    return RedirectToAction("Index", "Cart");
                }
                catch
                {
                    _dataContext.Database.RollbackTransaction();
                }
            }
            return View(model);
		}
    }
}
