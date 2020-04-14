using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
namespace DataAccess.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            MailAddress from = new MailAddress("kz.ebooksharing@gmail.com", "Администрация сайта");
            MailAddress to = new MailAddress(email);
            MailMessage _message = new MailMessage(from, to);
            _message.Subject = subject;
            _message.Body = message;
            _message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("kz.ebooksharing@gmail.com", "solune071");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(_message);
        }
    }
}
