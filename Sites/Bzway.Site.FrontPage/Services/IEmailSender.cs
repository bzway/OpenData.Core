using System.Threading.Tasks;

namespace Bzway.Site.FrontPage.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
