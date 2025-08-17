using SNUGGLEINN_CASESTUDY.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SNUGGLEINN_CASESTUDY.Repositories
{
    public class EmailService : IEmailService
    {
        private readonly string _fromEmail = "your-email@example.com";
        private readonly string _smtpHost = "smtp.example.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "your-email@example.com";
        private readonly string _smtpPass = "your-password";

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_smtpHost, _smtpPort)
            {
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true
            };

            var mail = new MailMessage(_fromEmail, to, subject, body)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(mail);
        }
    }
}
