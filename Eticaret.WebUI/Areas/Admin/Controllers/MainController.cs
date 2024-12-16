using Microsoft.AspNetCore.Mvc;

namespace Eticaret.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] // MainController Area Admin bölgesi içerisinde çalışacak
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
