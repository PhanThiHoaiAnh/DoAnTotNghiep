using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Models.Views;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;
using System.Security.Claims;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
	public class OrderCardController : Controller
	{
		private readonly DataContext _dataContext;
        private readonly IEmailSender _emailSender;
        public OrderCardController(DataContext dataContext, IEmailSender emailSender)
		{
			_dataContext = dataContext;
            _emailSender = emailSender;
        }
		public IActionResult Create()
		{
			ViewBag.WeddingCard = _dataContext.tblWeddingCardCategories.ToList();
			return View();
		}
        public IActionResult Success()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Create(WeddingCardModel card)
		{
			if (ModelState.IsValid)
			{
				ViewBag.WeddingCard = _dataContext.tblWeddingCardCategories.ToList();

				var userEmail = User.FindFirstValue(ClaimTypes.Email);
				if (userEmail == null)
				{
					return RedirectToAction("Login", "Account");
				}
				else
				{
					card.UserName = userEmail;
					var weddingCard = _dataContext.tblWeddingCardCategories.FirstOrDefault(m => m.Id == card.CardId);
					card.CardId = weddingCard.Id;
					card.Total = weddingCard.Price * card.Quantity;
					card.OrderCode = Guid.NewGuid().ToString();
                    card.OrderDate = DateTime.Now;
                }
				long deposit = 0;
				deposit = card.Total * 30 / 100;
                _dataContext.tblOrderWeddingCard.Add(card);
				await _dataContext.SaveChangesAsync();
                
                return RedirectToAction("CheckoutCard", new { orderCardId = card.Id });
            }
			
			return View(card);
		}
        public IActionResult CheckoutCard(int orderCardId, string payment)
        {
			var card = _dataContext.tblOrderWeddingCard.FirstOrDefault(p => p.Id == orderCardId);

			if (card != null)
			{
				var menu = _dataContext.tblWeddingCardCategories.FirstOrDefault(m => m.Id == card.CardId);
				ViewData["TenThiep"] = menu?.Name;

                DatTiecModel model = new DatTiecModel
				{
					// Gán giá trị từ OrdercardModel vào DatTiecModel
					UserName = card.UserName,
					FtName = card.FirstName,
					LtName = card.LastName,
					PhoneNumber= card.PhoneNumber,
					PartyCode = card.OrderCode,
					OrderDate = DateTime.Now,
					OrderOrg = card.ReceiveDate,
					Quantity = card.Quantity,
					Total = card.Total,
					Deposit = card.Total *30 /100,
					Pay = 0,
					Status = 0,
					Address = card.AddressReceived,
					ServiceName = menu?.Name,
					Payment = payment,
				};

				try
				{
                    _dataContext.Add(model);
                    _dataContext.SaveChanges();
                    OrderDetails cthd = new OrderDetails
                    {
                        OrderCode = model.PartyCode,
                        UserName = model.UserName,
                        ServiceId = card.CardId,
                        Price = menu.Price,
                        Quantity = model.Quantity,
                        ReceivedDate = model.OrderOrg,
                        Discount = 0
                    };
                    _dataContext.tblOrderDetails.Add(cthd);
                    _dataContext.SaveChanges();
                    _emailSender.SendEmailAsync(model.UserName, "Xác nhận đơn hàng", $"Bạn đã đặt thành công đơn đặt thiệp: {model.PartyCode}\nLoại thiệp:{model.ServiceName} - Đơn giá:{menu.Price.ToString("N0")}\nTổng giá trị đơn hàng :{model.Total.ToString("N0")}\nTiền cọc(30% tổng đơn hàng):{model.Deposit.ToString("##,##")}\nQuý khách vui lòng hoàn thành thanh toán tiền cọc tại cơ sở dịch vụ trong vòng 3 ngày kể từ ngày đặt hàng\nXin cảm ơn quý khách!");
                    return View("CheckoutCard", model);
                }
                catch (Exception exc)
				{
                    ModelState.AddModelError("", "Có vài lỗi xảy ra. Bạn vui lòng quay lại sau.");
                    return View(model);
                }
            }
			else
			{
				// Xử lý khi không tìm thấy tiệc theo partyId
				return RedirectToAction("Create");
			}
		}
	}
}
