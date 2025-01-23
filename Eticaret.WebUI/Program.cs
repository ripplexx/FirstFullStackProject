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
                x.LoginPath = "/Account/SignIn"; // Giri� yapma sayfas�
                x.LogoutPath = "/Account/SignOut"; // ��k�� yapma sayfas�
                x.AccessDeniedPath = "/Account/AccessDenied"; // Eri�im reddedildi sayfas�
                x.Cookie.Name = "Account"; // �erez ad�
                x.Cookie.MaxAge = TimeSpan.FromDays(60); // �erez s�resi g�n olarak
                x.Cookie.IsEssential = true; // Zorunlu �erez
            }); // Kimlik do�rulama
            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin")); // Admin yetkisi. Admin yetkisi olanlar
                x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin","User","Customer")); // User yetkisi. Admin, User ve Customer yetkisi olanlar
            }); // Yetkilendirme i�in

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

            app.UseAuthentication(); // Buras� �nce gelecek. �nce oturum a�ma
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

