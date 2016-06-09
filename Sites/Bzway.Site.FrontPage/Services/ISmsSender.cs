using System.Threading.Tasks;

namespace Bzway.Site.FrontPage.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
