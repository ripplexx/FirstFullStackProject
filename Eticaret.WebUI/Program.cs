using Eticaret.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
namespace Eticaret.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DatabaseContext>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
            {
                x.LoginPath = "/Account/SignIn"; // Giriþ yapma sayfasý
                x.LogoutPath = "/Account/SignOut"; // Çýkýþ yapma sayfasý
                x.AccessDeniedPath = "/Account/AccessDenied"; // Eriþim reddedildi sayfasý
                x.Cookie.Name = "Account"; // Çerez adý
                x.Cookie.MaxAge = TimeSpan.FromDays(60); // Çerez süresi gün olarak
                x.Cookie.IsEssential = true; // Zorunlu çerez
            }); // Kimlik doðrulama
            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin")); // Admin yetkisi. Admin yetkisi olanlar
                x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin","User","Customer")); // User yetkisi. Admin, User ve Customer yetkisi olanlar
            }); // Yetkilendirme için

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Burasý önce gelecek. Önce oturum açma
            app.UseAuthorization(); // Sonra yetkilendirme

            app.MapControllerRoute(
                        name: "admin",
                        pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}

