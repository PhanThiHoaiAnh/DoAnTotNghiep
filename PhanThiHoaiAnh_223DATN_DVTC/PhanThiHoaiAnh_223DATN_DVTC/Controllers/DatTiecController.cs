using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Models.Views;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;
using PhanThiHoaiAnh_223DATN_DVTC.Services;
using System.IO;
using System.Security.Claims;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
    public class DatTiecController : Controller
    {
        private readonly DataContext _dataContext;
		public DatTiecController(DataContext _context)
		{
			_dataContext = _context;

        }
        public IActionResult Create()
        {
			ViewBag.PartyCategories = new SelectList(_dataContext.tblPartyCategories, "Id", "Name");
			ViewBag.Location = new SelectList(_dataContext.tblLocation, "Id", "Name");
			ViewBag.Menus = _dataContext.tblMenu.ToList();
			return View();
        }
		public IActionResult DatTiec()
		{
			ViewBag.PartyCategories = new SelectList(_dataContext.tblPartyCategories, "Id", "Name");
			ViewBag.Location = new SelectList(_dataContext.tblLocation, "Id", "Name");
			ViewBag.Menus = _dataContext.tblMenu.ToList();
            ViewBag.Service = _dataContext.tblOtherServices.ToList();
			return View();
		}
        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }

        [HttpGet]
		public IActionResult PaymentCallBack()
		{
			return View();
		}
        [HttpPost]
        public IActionResult Confirm(int partyId, PartyViewModel model ,string payment = "COD")
        {
            if(ModelState.IsValid) 
            { 
                var party = _dataContext.tblParty.FirstOrDefault(p => p.Id == partyId);
                     
                //.Include(p => p.ThucDon);
                if (party != null)
                {
                    var menuName = _dataContext.tblMenu.FirstOrDefault(m => m.Id == party.MenuParty)?.Name;
                    ViewData["TenThucDon"] = menuName;
                    
                    DatTiecModel hoaDon = new DatTiecModel
                    {
                        // Gán giá trị từ PartyModel vào DatTiecModel
                        UserName = party.UserName,
                        PartyCode = party.PartyCode,
                        OrderDate = party.OrderDate,
                        OrderOrg = party.OrgDate,
                        Quantity = party.NumTable,
                        Total = party.Total,
                        Deposit = party.Deposit,
                        Pay = party.Pay,
                        Status = 0,
                        Address = party.LocationName,
                        ServiceName = menuName,
                        Payment = payment,
                    };
					_dataContext.Add(model);
					_dataContext.SaveChanges();
                    
                    
                }
                else
                {
                    // Xử lý khi không tìm thấy tiệc theo partyId
                    return RedirectToAction("Create");
                }
            }
            return Redirect("Success");
        }
 
        [HttpPost]
		public async Task<IActionResult> Create(PartyModel party)
		{
			if (ModelState.IsValid)
			{
                // Nếu dữ liệu không hợp lệ, trả về view với model để hiển thị lỗi
                ViewBag.PartyCategories = new SelectList(_dataContext.tblPartyCategories, "Id", "Name", party.PartyCategoryId);
                ViewBag.Location = new SelectList(_dataContext.tblLocation, "Id", "Name", party.LocationId);
                ViewBag.Menus = _dataContext.tblMenu.ToList();

                var userEmail = User.FindFirstValue(ClaimTypes.Email);
				if (userEmail == null)
				{
					return RedirectToAction("Login", "Account");
				}
				else
				{
                    party.UserName = userEmail;
					party.PartyCode = Guid.NewGuid().ToString();
                    party.OrderDate = DateTime.Now;
					var menu = _dataContext.tblMenu.FirstOrDefault(m => m.Id == party.MenuParty);
					party.NumTable = party.CusNumber / party.PersonTable;
					party.Total = menu.Price * party.NumTable;
					party.Pay = party.Total * 10 /100;//thuế
                    party.Deposit = (party.Total + party.Pay)*30 /100;
					party.Status = false;
				}

                _dataContext.tblParty.Add(party);
                await _dataContext.SaveChangesAsync();
            }
            return RedirectToAction("CheckoutParty", new { partyId = party.Id });
        }
        public IActionResult CheckoutParty(int partyId, DatTiecModel model, string payment)
        {
            if (ModelState.IsValid)
            {
                var party = _dataContext.tblParty.FirstOrDefault(p => p.Id == partyId);

                //.Include(p => p.ThucDon);
                if (party != null)
                {
                    var menuName = _dataContext.tblMenu.FirstOrDefault(m => m.Id == party.MenuParty)?.Name;
                    ViewData["TenThucDon"] = menuName;

                    model = new DatTiecModel
                    {
                        // Gán giá trị từ PartyModel vào DatTiecModel
                        UserName = party.UserName,
                        PartyCode = party.PartyCode,
                        OrderDate = party.OrderDate,
                        OrderOrg = party.OrgDate,
                        Quantity = party.NumTable,
                        Total = party.Total,
                        Deposit = party.Deposit,
                        Pay = party.Pay,
                        Status = 0,
                        Address = party.LocationName,
                        ServiceName = menuName,
                        Payment = payment,
                    };

                    _dataContext.Add(model);
                    _dataContext.SaveChanges();
                    var menu = _dataContext.tblMenu.FirstOrDefault(m => m.Id == party.MenuParty);
                    OrderDetails cthd = new OrderDetails
                    {
                        OrderCode = model.PartyCode,
                        UserName = model.UserName,
                        ServiceId = party.MenuParty,
                        Price = menu.Price,
                        Quantity = party.NumTable,
                        ReceivedDate = model.OrderOrg,
                        Discount = 0
                    };
                    _dataContext.tblOrderDetails.Add(cthd);
                    _dataContext.SaveChanges();

                    return View(model);
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
