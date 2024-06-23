using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Models.Views;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;
using PhanThiHoaiAnh_223DATN_DVTC.Services;
using System.Security.Claims;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
	public class CartController : Controller
	{
		private readonly DataContext _dataContext;
        private readonly IEmailSender _emailSender;
        private readonly PaypalClient _paypalClient;

        public CartController(DataContext _context, IEmailSender emailSender, PaypalClient paypalClient)
		{
			_dataContext = _context;
			_emailSender = emailSender;
			_paypalClient = paypalClient;
		}
		public IActionResult Index()
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			return View(cart);
		}
		public async Task<IActionResult> Add(int Id, int quantity = 1)
		{
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            OtherServicesModel service = await _dataContext.tblOtherServices.FindAsync(Id);
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
			FoodModel food = await _dataContext.tblFood.FindAsync(Id);

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
			MenuModel menu = await _dataContext.tblMenu.FindAsync(Id);

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
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            if (cart.Count == 0)
			{
				return Redirect("/");
			}
            ViewBag.CheckCart = cart;
			ViewBag.PaypalClientid = _paypalClient.ClientId;
            return View(cart);
		}
		public IActionResult Success()
		{
			return View();
		}

		[Authorize]
        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model, string paymentStatus)
		{
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

			if (userEmail != null )
			{
				var hoaDon = new DatTiecModel();

				hoaDon.PartyCode = Guid.NewGuid().ToString();
				hoaDon.FtName = model.FName;
				hoaDon.LtName = model.LName;
				hoaDon.UserName = userEmail;
				hoaDon.Address = model.Address;
				hoaDon.PhoneNumber = model.PhoneNumber;
				hoaDon.OrderDate = DateTime.Now;
				hoaDon.OrderOrg = model.OrgDate;
				hoaDon.Payment = "COD";
				hoaDon.Status = 0;
				hoaDon.Note = model.Note;
				try
				{
					//them hoaDon
					_dataContext.tblOrder.Add(hoaDon);
					_dataContext.SaveChanges();

					var id = hoaDon.Id;
					List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
					long ttVAT = 0, total = 0, deposit = 0;
					foreach (var item in cart)
					{
						var cthd = new OrderDetails();
						cthd.OrderCode = hoaDon.PartyCode;
						cthd.UserName = hoaDon.UserName;
						cthd.ServiceId = item.ServiceId;
						cthd.Price = item.Price;
						cthd.Quantity = item.Quantity;
						cthd.ReceivedDate = hoaDon.OrderDate;
						cthd.Discount = 0;
						_dataContext.tblOrderDetails.Add(cthd);
						_dataContext.SaveChanges();
						total += (item.Price * item.Quantity);
						//ttVAT = total + (total * 10 / 100);
						deposit = total * 30 / 100;

					}
					_emailSender.SendEmailAsync(userEmail, "Xác nhận đơn hàng", $"Bạn đã đặt thành công đơn: {hoaDon.PartyCode}\nTổng giá trị đơn hàng :{total.ToString("##,##")}\nTiền cọc(30% tổng đơn hàng):{deposit.ToString("##,##")}\n Xin cảm ơn quý khách!");
					HttpContext.Session.Remove("Cart");
					return RedirectToAction("Success", "Cart");

				} catch (Exception ex)
				{
					return RedirectToAction("Cart");
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

                
            }
            return View(model);
		}
    }
}
