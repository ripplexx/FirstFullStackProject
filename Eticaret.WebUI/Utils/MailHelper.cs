using Eticaret.Core.Entities;
using System.Net;
using System.Net.Mail;

namespace Eticaret.WebUI.Utils
{
    public class MailHelper
    {
        public static async Task SendMailAsync(Contact contact)
        {
            SmtpClient smtpClient = new SmtpClient("mail.siteadi.com", 587);
            smtpClient.Credentials = new NetworkCredential("info@siteadi.com","mailsifresi");
            smtpClient.EnableSsl = true; // ssl kullanılıyorsa true, değilse false yapın(Gmail ssl kullanıyor)
            MailMessage message = new MailMessage();
            message.From = new MailAddress("info@siteadi.com");
            message.To.Add("bilgi@siteadi.com");
            message.Subject = "Yeni Mesaj";
            message.Body = $"Adı: {contact.Name} {contact.Surname} <br/> Email: {contact.Email} <br/> Telefon: {contact.Phone} <br/> Mesaj: {contact.Message}";
            message.Subject = "";
            message.IsBodyHtml = true; // html kodlarını desteklemiş olur.
            await smtpClient.SendMailAsync(message);
            smtpClient.Dispose();
        }
    }
}
