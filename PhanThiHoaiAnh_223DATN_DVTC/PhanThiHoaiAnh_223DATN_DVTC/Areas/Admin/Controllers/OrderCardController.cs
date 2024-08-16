using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Member")]
    public class OrderCardController : Controller
	{
        private readonly DataContext _dataContext;
        public OrderCardController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index()
        {
            var cards = await _dataContext.tblOrderWeddingCard.Include(s => s.CardModel).ToListAsync();
            foreach (var card in cards)
            {
                if (card.CardId != null)
                {
                    var thiep = await _dataContext.tblWeddingCardCategories.FindAsync(card.CardId);
                    card.CardModel = thiep;
                }
            }
            return View(cards);
        }
        public async Task<IActionResult> Inspect(int id)
        {
            var card = await _dataContext.tblOrderWeddingCard.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            card.Status = true; // Thay đổi trạng thái thành true
            _dataContext.Update(card);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int Id)
        {
            WeddingCardModel card = await _dataContext.tblOrderWeddingCard.FindAsync(Id);
            _dataContext.tblOrderWeddingCard.Remove(card);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Đơn hàng đã được xóa";
            return RedirectToAction("Index");
        }
    }
}
