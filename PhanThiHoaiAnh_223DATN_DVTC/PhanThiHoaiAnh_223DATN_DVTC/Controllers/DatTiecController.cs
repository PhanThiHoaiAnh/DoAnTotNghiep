using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender _emailSender;
        public DatTiecController(DataContext _context, IEmailSender emailSender)
		{
			_dataContext = _context;
            _emailSender = emailSender;

        }
        public IActionResult Create(List<FoodModel> selectedFoodItems)
        {
			ViewBag.PartyCategories = new SelectList(_dataContext.tblPartyCategories, "Id", "Name");
			ViewBag.Location = new SelectList(_dataContext.tblLocation, "Id", "Name");
			ViewBag.Menus = _dataContext.tblMenu.ToList();
            // Sử dụng danh sách các món ăn đã chọn để hiển thị trên trang Index
            ViewBag.SelectedFoodItems = selectedFoodItems;
            // Lấy danh sách món ăn từ cơ sở dữ liệu
            List<OtherServicesModel> availableFoodItems = _dataContext.tblOtherServices.ToList();

            PartyModel model = new PartyModel
            {
                otherService = availableFoodItems
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartyModel party)
        {
            if (ModelState.IsValid)
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                if (userEmail == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    // Nếu dữ liệu không hợp lệ, trả về view với model để hiển thị lỗi
                    ViewBag.PartyCategories = new SelectList(_dataContext.tblPartyCategories, "Id", "Name", party.PartyCategoryId);
                    ViewBag.Location = new SelectList(_dataContext.tblLocation, "Id", "Name", party.LocationId);
                    ViewBag.Menus = _dataContext.tblMenu.ToList();
                    // Lấy danh sách các dịch vụ đã chọn
                    List<OtherServicesModel> selectedServiceItems = _dataContext.tblOtherServices
                                            .Where(fi => party.SelectedServiceItems.Contains(fi.Id))
                                            .ToList();
                    // Tính tổng giá của các món ăn đã chọn
                    long totalPrice = selectedServiceItems.Sum(fi => fi.Price);

                    // Tạo biến selectedFoodItemsString và gán giá trị
                    string selectedServiceItemsString = string.Join(",", party.SelectedServiceItems);

                    // Chuyển đổi danh sách các ID món ăn từ chuỗi thành List<int>
                    List<int> parsedSelectedFoodItems = selectedServiceItemsString//.ToList();//selectedFoodItemsString
                        .Split(',')
                        .Select(int.Parse)
                        .ToList();
                    // Lưu vào cơ sở dữ liệu
                    var menu = _dataContext.tblMenu.FirstOrDefault(m => m.Id == party.MenuParty);
                    PartyModel partyMd = new PartyModel
                    {
                        UserName = userEmail,
                        PartyCode = Guid.NewGuid().ToString(),
                        OrderDate = DateTime.Now,
                        OrgDate = party.OrgDate,
                        Time = party.Time,
                        FirstName = party.FirstName,
                        LastName = party.LastName,
                        LocationName = party.LocationName,
                        PartyCategoryId = party.PartyCategoryId,
                        LocationId = party.LocationId,
                        PersonTable = party.PersonTable,
                        CusNumber = party.CusNumber,
                        PhoneNumber = party.PhoneNumber,
                        MenuParty = party.MenuParty,
                        NumTable = party.CusNumber / party.PersonTable,
                        Status = false,
                        FoodName = menu.SelectedFoodItemNames,
                        SelectedServiceItems = parsedSelectedFoodItems,
                    };

                    partyMd.Total = menu.Price * partyMd.NumTable + totalPrice;
                    partyMd.Pay = partyMd.Total * 10 / 100;//thuế
                    partyMd.Deposit = (partyMd.Total + partyMd.Pay) * 30 / 100;
                    // Lấy danh sách tên các dịch vụ đã chọn
                    List<string> selectedServiceItemNames = _dataContext.tblOtherServices
                    .Where(fi => party.SelectedServiceItems.Contains(fi.Id))
                    .Select(fi => fi.Name)
                    .ToList();

                    // Gán danh sách tên các món ăn vào menu.SelectedFoodItemNames
                    partyMd.otherServiceName = selectedServiceItemNames;
                   // partyMd.Id = partyMd.Id;

                    _dataContext.tblParty.Add(partyMd);
                    await _dataContext.SaveChangesAsync();

					await _emailSender.SendEmailAsync(partyMd.UserName, "Xác nhận đơn hàng", $"Bạn đã đặt thành công đơn đặt tiệc: {partyMd.PartyCode} và các dịch vụ kèm theo\nTổng giá trị đơn hàng :{partyMd.Total.ToString("N0")}\nTiền cọc(30% tổng đơn hàng):{partyMd.Deposit.ToString("##,##")}\nQuý khách vui lòng hoàn thành thanh toán tiền cọc tại cơ sở dịch vụ trong vòng 3 ngày kể từ ngày đặt hàng\nXin cảm ơn quý khách!");
					return RedirectToAction("CheckoutParty", new { partyId = partyMd.Id });
                }
            }
            party.otherService = _dataContext.tblOtherServices.ToList();
            return View(party);
        }
        public IActionResult CheckoutParty(int partyId, string payment)
        {
            var party = _dataContext.tblParty.FirstOrDefault(p => p.Id == partyId);
            
            if (party != null)
            {
                var menu = _dataContext.tblMenu.FirstOrDefault(m => m.Id == party.MenuParty);
                ViewData["TenThucDon"] = menu?.Name;

                DatTiecModel hoaDon = new DatTiecModel
                {
                    // Gán giá trị từ PartyModel vào DatTiecModel
                    UserName = party.UserName,
                    FtName = party.FirstName,
                    LtName = party.LastName,
                    PartyCode = party.PartyCode,
                    OrderDate = party.OrderDate,
                    OrderOrg = party.OrgDate,
                    Quantity = party.NumTable,
                    Total = party.Total,
                    Deposit = party.Deposit,
                    Pay = party.Pay,
                    Status = 0,
                    Address = party.LocationName,
                    ServiceName = menu?.Name,
                    Payment = payment,
                    FoodList = party.FoodName,
                    ServiceList = party.otherServiceName
                };

                try
                {
                    _dataContext.Add(hoaDon);
                    _dataContext.SaveChanges();
                    OrderDetails cthd = new OrderDetails
                    {
                        OrderCode = hoaDon.PartyCode,
                        UserName = hoaDon.UserName,
                        ServiceId = party.MenuParty,
                        Price = menu.Price,
                        Quantity = party.NumTable,
                        ReceivedDate = hoaDon.OrderOrg,
                        Discount = 0
                    };
                    _dataContext.tblOrderDetails.Add(cthd);
                    _dataContext.SaveChanges();
                    _emailSender.SendEmailAsync(hoaDon.UserName, "Xác nhận đơn hàng", $"Bạn đã đặt thành công đơn đặt tiệc: {hoaDon.PartyCode}\nTên thực đơn:{hoaDon.ServiceName} và các dịch vụ kèm theo\nTổng giá trị đơn hàng :{hoaDon.Total.ToString("N0")}\nTiền cọc(30% tổng đơn hàng):{hoaDon.Deposit.ToString("##,##")}\nQuý khách vui lòng hoàn thành thanh toán tiền cọc tại cơ sở dịch vụ trong vòng 3 ngày kể từ ngày đặt hàng\nXin cảm ơn quý khách!");

                    return Redirect("Success");
                }
                catch (Exception exc)
                {
                    ModelState.AddModelError("", "Có vài lỗi xảy ra. Bạn vui lòng quay lại sau.");
                    return View(hoaDon);
                }
            }
            else
            {
                // Xử lý khi không tìm thấy tiệc theo partyId
                return RedirectToAction("Create");
            }
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
						FtName = party.FirstName,
						LtName = party.LastName,
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
        
    }
}
