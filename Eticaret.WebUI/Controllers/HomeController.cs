using Eticaret.Core.Entities;
using Eticaret.Data;
using Eticaret.WebUI.Models;
using Eticaret.WebUI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Eticaret.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel()
            {
                Sliders = await _context.Sliders.ToListAsync(),
                Products = await _context.Products.Where(p=>p.IsActive && p.IsHome).ToListAsync(),// burada sadece anasayfada olan �r�nleri getirir.Filtreleme yapar.
                News = await _context.News.ToListAsync(),
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("AccessDenied")] // Buras� bir route tan�mlamas�d�r. Bu sayfaya eri�im izni olmayan kullan�c�lar y�nlendirilir.
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactUsAsync(Contact contact)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _context.Contacts.AddAsync(contact);
                    var sonuc=await _context.SaveChangesAsync();
                    /*if(sonuc > 0)
                    {
                        ViewBag.Message = "Mesaj�n�z ba�ar�yla al�nd�.";
                    }
                    else
                    {
                        ViewBag.Message = "Mesaj�n�z al�namad�.";
                    }*/
                    if(sonuc > 0)
                    {
                        TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                         <strong>Mesaj�n�z ba�ar�yla al�nd�!</strong> 
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div>";
                        //await MailHelper.SendMailAsync(contact);
                        return RedirectToAction("ContactUs");
                    }
                   
                }
                catch(Exception)
                {
                    ModelState.AddModelError("", "Bir hata olu�tu. L�tfen tekrar deneyiniz.");
                }
                
            }
            return View(contact);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
