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
		public OrderCardController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		public IActionResult Create()
		{
			ViewBag.WeddingCard = _dataContext.tblWeddingCardCategories.ToList();
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
					card.Total = weddingCard.Price * card.Quantity;
					card.OrderCode = Guid.NewGuid().ToString();
				}

				_dataContext.tblOrderWeddingCard.Add(card);
				await _dataContext.SaveChangesAsync();
			}
			return RedirectToAction("CheckoutCard", new { orderCardId = card.Id });
			//return RedirectToAction("Confirm", new { partyId = party.Id });
		}
		public IActionResult CheckoutCard(int orderCardId, DatTiecModel model, string payment)
		{
			if (ModelState.IsValid)
			{
				var card = _dataContext.tblOrderWeddingCard.FirstOrDefault(p => p.Id == orderCardId);

				if (card != null)
				{
					var menuName = _dataContext.tblWeddingCardCategories.FirstOrDefault(m => m.Id == card.CardId)?.Name;
					ViewData["TenThiep"] = menuName;

					model = new DatTiecModel
					{
						// Gán giá trị từ PartyModel vào DatTiecModel
						UserName = card.UserName,
						PartyCode = card.OrderCode,
						OrderDate = DateTime.Now,
						OrderOrg = card.ReceiveDate,
						Quantity = card.Quantity,
						Total = card.Total,
						Deposit = card.Total *30 /100,
						Pay = 0,
						Status = 0,
						Address = card.AddressReceived,
						ServiceName = menuName,
						Payment = payment,
					};

					_dataContext.Add(model);
					_dataContext.SaveChanges();
					var menu = _dataContext.tblWeddingCardCategories.FirstOrDefault(m => m.Id == card.CardId);
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

					return Redirect("Success");
                }
				else
				{
					// Xử lý khi không tìm thấy tiệc theo partyId
					return RedirectToAction("Create");
				}
			}
			return View(model);
		}

	}
}
