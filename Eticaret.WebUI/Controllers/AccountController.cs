using Eticaret.Core.Entities;
using Eticaret.Data;
using Eticaret.WebUI.Models;
using Microsoft.AspNetCore.Authentication; // login işlemleri için
using Microsoft.AspNetCore.Authorization; // login işlemleri için
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims; // Kullanıcı bilgileri için

namespace Eticaret.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;

        public AccountController(DatabaseContext context)
        {
            _context = context;
        }

        [Authorize] // Giriş yapmış kullanıcılar için
        public IActionResult Index() // Anasayfa
        {
            return View();
        }
        public IActionResult SignIn() // Giriş yapma sayfası
        {
            return View();
        }

        [HttpPost] // Giriş yapma işlemi
        public async Task<IActionResult> SignInAsync(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
               try
                {
                    var account = await _context.AppUsers.FirstOrDefaultAsync(x=>x.Email== loginViewModel.Email & x.Password==loginViewModel.Password & x.IsActive );
                    if (account == null)
                    {
                        ModelState.AddModelError("", "Giriş Başarısız!");
                    }
                    else 
                    { 
                        var claims = new List<Claim>() // Kullanıcı bilgileri
                        {
                            new Claim(ClaimTypes.Name, account.Name), // Adı
                            new Claim(ClaimTypes.Role, account.IsAdmin ? "Admin" : "User"), // Admin ise Admin, değilse User
                            new Claim(ClaimTypes.Email, account.Email), // E-posta
                            new ("UserId", account.Id.ToString()), // Id
                            new ("UserGuid", account.UserGuid.ToString()), // Guid
                        };
                        var userIdentity = new ClaimsIdentity(claims, "Login"); // Kullanıcı kimliği
                        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity); // Kullanıcı ilkesi
                        await HttpContext.SignInAsync(userPrincipal); // Kullanıcıyı giriş yap
                        return Redirect(string.IsNullOrEmpty(loginViewModel.ReturnUrl) ? "/" : loginViewModel.ReturnUrl); // Anasayfaya yönlendir
                    }
                
                }
                catch (Exception hata) 
                {
                    // Loglama
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                }

            }
            return View(loginViewModel);
        }
        public IActionResult SignUp() // Kayıt olma sayfası
        {
            return View();
        }

        [HttpPost] // Kayıt olma işlemi
        public async Task<IActionResult> SignUpAsync(AppUser appUser) 
        {
            appUser.IsAdmin = false; // Admin olmayacak
            appUser.IsActive = true; // Aktif olacak
            if (ModelState.IsValid)
            {
                await _context.AddAsync(appUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
           
        }
        public async Task<IActionResult> SignOutAsync() // Çıkış yapma işlemi
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn");
        }
  
    }
}
