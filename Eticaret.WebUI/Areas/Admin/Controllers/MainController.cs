using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eticaret.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")] // MainController Area Admin bölgesi içerisinde çalışacak
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
