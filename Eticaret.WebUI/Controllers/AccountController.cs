using Microsoft.AspNetCore.Mvc;

namespace Eticaret.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index() // Anasayfa
        {
            return View();
        }
        public IActionResult SignIn() // Giriş yapma sayfası
        {
            return View();
        }
        public IActionResult SignUp() // Kayıt olma sayfası
        {
            return View();
        }
    }
}
