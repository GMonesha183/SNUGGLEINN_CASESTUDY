using System.Threading.Tasks;

namespace SNUGGLEINN_CASESTUDY.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
