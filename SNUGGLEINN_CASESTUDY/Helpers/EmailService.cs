using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace SNUGGLEINN_CASESTUDY.Helpers
{
    public class EmailService
    {
        private readonly string _smtpHost = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "your-email@gmail.com"; // Change
        private readonly string _smtpPass = "your-app-password"; // Change

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using (var smtp = new SmtpClient(_smtpHost, _smtpPort))
            {
                smtp.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                smtp.EnableSsl = true;

                var message = new MailMessage(_smtpUser, to, subject, body);
                message.IsBodyHtml = true;
                await smtp.SendMailAsync(message);
            }
        }
    }
}
