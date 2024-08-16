using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Member")]
    public class PartyController : Controller
    {
        private readonly DataContext _dataContext;
        public PartyController(DataContext context)
        {
            _dataContext = context;

        }
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Index()
        {
            var parties = await _dataContext.tblParty.Include(s => s.ThucDon).ToListAsync();
            foreach (var party in parties)
            {
                if (party.MenuParty != null)
                {
                    var thucDon = await _dataContext.tblMenu.FindAsync(party.MenuParty);
                    party.ThucDon = thucDon;
                }
            }
            return View(parties);
        }
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Inspect(int id)
        {
            var party = await _dataContext.tblParty.FindAsync(id);
            if (party == null)
            {
                return NotFound();
            }

            party.Status = true; // Thay đổi trạng thái thành true
            _dataContext.Update(party);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int Id)
        {
            PartyModel food = await _dataContext.tblParty.FindAsync(Id);
            _dataContext.tblParty.Remove(food);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Đơn tiệc đã được xóa";
            return RedirectToAction("Index");
        }
    }
}
