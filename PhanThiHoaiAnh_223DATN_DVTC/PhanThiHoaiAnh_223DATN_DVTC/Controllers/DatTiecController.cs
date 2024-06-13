using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
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
			ViewBag.PartyCategories = new SelectList(_dataContext.PartyCategories, "Id", "Name");
			ViewBag.Location = new SelectList(_dataContext.Location, "Id", "Name");
			ViewBag.Menus = _dataContext.Menus.ToList();
			return View();
        }
		public IActionResult DatTiec()
		{
			ViewBag.PartyCategories = new SelectList(_dataContext.PartyCategories, "Id", "Name");
			ViewBag.Location = new SelectList(_dataContext.Location, "Id", "Name");
			ViewBag.Menus = _dataContext.Menus.ToList();
            ViewBag.Service = _dataContext.OtherServices.ToList();
			return View();
		}

		[HttpGet]
		public IActionResult PaymentCallBack()
		{
			return View();
		}
        public IActionResult Confirm(int partyId, DatTiecModel model ,string payment)
        {
            if(ModelState.IsValid) { 
                var party = _dataContext.Party.FirstOrDefault(p => p.Id == partyId);
                     
                //.Include(p => p.ThucDon);
                if (party != null)
                {
                    var menuName = _dataContext.Menus.FirstOrDefault(m => m.Id == party.MenuParty)?.Name;
                    ViewData["TenThucDon"] = menuName;
                    
                    model = new DatTiecModel
                    {
                        // Gán giá trị từ PartyModel vào DatTiecModel
                        UserName = party.UserName,
                        PartyCode = party.PartyCode,
                        OrderDate = party.OrderDate,
                        OrderOrg = party.OrgDate,
                        TableCount = party.NumTable,
                        Total = party.Total,
                        Deposit = party.Deposit,
                        Pay = party.Pay,
                        Status = false,
                        Address = party.LocationName,
                        ServiceName = menuName,
                        Payment = payment,
                    };
                    
                    _dataContext.Add(model);
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
 
        [HttpPost]
		public async Task<IActionResult> Create(PartyModel party)
		{
			if (ModelState.IsValid)
			{
                // Nếu dữ liệu không hợp lệ, trả về view với model để hiển thị lỗi
                ViewBag.PartyCategories = new SelectList(_dataContext.PartyCategories, "Id", "Name", party.PartyCategoryId);
                ViewBag.Location = new SelectList(_dataContext.Location, "Id", "Name", party.LocationId);
                ViewBag.Menus = _dataContext.Menus.ToList();

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
					var menu = _dataContext.Menus.FirstOrDefault(m => m.Id == party.MenuParty);
					party.NumTable = party.CusNumber / party.PersonTable;
					party.Total = menu.Price * party.NumTable;
					party.Deposit = party.Total * 30 / 100;
					party.Pay = party.Total - party.Deposit;
				}

                _dataContext.Party.Add(party);
                await _dataContext.SaveChangesAsync();
            }
            //return RedirectToAction("ConfirmParty", "DatTiec");
            return RedirectToAction("Confirm", new { partyId = party.Id });
        }
        public IActionResult CompletePayment(string orderId)
        {
            return RedirectToAction("Success","Cart");
        }
        [HttpPost]
        public async Task<IActionResult> DatTiec(PartyModel party, List<int> service)
        {
            if (ModelState.IsValid)
            {
                // Nếu dữ liệu không hợp lệ, trả về view với model để hiển thị lỗi
                ViewBag.PartyCategories = new SelectList(_dataContext.PartyCategories, "Id", "Name", party.PartyCategoryId);
                ViewBag.Location = new SelectList(_dataContext.Location, "Id", "Name", party.LocationId);
                ViewBag.Menus = _dataContext.Menus.ToList();

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
                    var menu = _dataContext.Menus.FirstOrDefault(m => m.Id == party.MenuParty);
                    party.NumTable = party.CusNumber / party.PersonTable;
                    party.Total = menu.Price * party.NumTable;
                    party.Deposit = party.Total * 30 / 100;
                    party.Pay = party.Total - party.Deposit;
                    //party.SelectedService = new SelectList<OtherServicesModel>();

                }

                _dataContext.Party.Add(party);
                await _dataContext.SaveChangesAsync();
            }
            //return RedirectToAction("ConfirmParty", "DatTiec");
            return RedirectToAction("Confirm", new { partyId = party.Id });
        }
    }
}
