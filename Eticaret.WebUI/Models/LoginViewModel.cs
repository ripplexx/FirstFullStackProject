using System.ComponentModel.DataAnnotations;

namespace Eticaret.WebUI.Models
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress), Required(ErrorMessage = "Email Boş Geçilemez!")]
        public string Email { get; set; }
        [Display(Name = "Şifre")] 
        [DataType(DataType.Password), Required(ErrorMessage = "Şifre Boş Geçilemez!")] // Şifre tipinde olacak
        public string Password { get; set; }
        public string? ReturnUrl { get; set; } // Dönüş URL'si
        public bool RememberMe { get; set; } // Beni hatırla


    }
}
