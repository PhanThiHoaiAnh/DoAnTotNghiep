using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace PhanThiHoaiAnh_223DATN_DVTC.Controllers
{
	public class IntroduceController :Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
