using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.Sig;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using PhanThiHoaiAnh_223DATN_DVTC.Repository;
using PhanThiHoaiAnh_223DATN_DVTC.Services;
using System.Reflection.Metadata;
using Document = iTextSharp.text.Document;

namespace PhanThiHoaiAnh_223DATN_DVTC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderPartyController : Controller
    {
        private readonly DataContext _dataContext;
        private IViewRenderService _viewRenderService;
        public OrderPartyController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.OrderParty.OrderByDescending(s => s.Id).ToListAsync());
        }
        public async Task<IActionResult> Detail(int id)
        {
            var oder = await _dataContext.OrderParty.FirstOrDefaultAsync(s=> s.Id == id);
            if (id == null)
            {
                return NotFound();
            }
            return View(oder);
        }
        private async Task<string> Export(int id)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var model = _dataContext.OrderParty.FindAsync(id);
            var filename = "PdfExport" + DateTime.Now.ToString("ddMMyyyyhhmm");
            string html = await _viewRenderService.RenderToStringAsync("_pdfDemo",model);
            Document document = new Document();
            XMLWorkerFontProvider fontProvider = new XMLWorkerFontProvider(XMLWorkerFontProvider.DONTLOOKFORFONTS);
            var path = Directory.GetCurrentDirectory()+"/wwwroot/" + filename +".pdf";
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + "wwwroot/admin"))
            {
                FontFactory.FontImp.Register(file);
            }

            document.Open();

            using(var strReader = new StringReader(html))
            {
                //set factories
                HtmlPipelineContext htmlContext = new HtmlPipelineContext(null);
                htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
                //set css
                ICSSResolver cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                cssResolver.AddCssFile(Directory.GetCurrentDirectory() + "/wwwroot/css/site.css", true);
                //Export
                IPipeline pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext, new PdfWriterPipeline(document,writer)));
                var worker = new XMLWorker(pipeline, true);
                var xmlParse = new XMLParser(true,worker);
                xmlParse.Parse(strReader);
                xmlParse.Flush();
            }
            document.Close();
            return path;
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int Id)
        {
            DatTiecModel food = await _dataContext.OrderParty.FindAsync(Id);
            return View(food);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, DatTiecModel food)
        {
            if (ModelState.IsValid)
            {
                //them du lieu
                _dataContext.Update(food);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Cập nhật địa điểm thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Model có một vài thứ đang bị lỗi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
            return View(food);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            DatTiecModel food = await _dataContext.OrderParty.FindAsync(Id);
            _dataContext.OrderParty.Remove(food);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Đơn tiệc đã được xóa";
            return RedirectToAction("Index");
        }

    }
}
