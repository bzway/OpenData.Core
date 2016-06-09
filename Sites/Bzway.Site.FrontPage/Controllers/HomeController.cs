using Microsoft.AspNetCore.Mvc;
using Bzway.Module.Core;

namespace Bzway.Site.FrontPage.Controllers
{
    public class HomeController : Controller
    {
        IUserSite site;
        public HomeController(IUserSite site)
        {
            this.site = site;
        }
        public IActionResult Index()
        {

            if (site != null)
            {
                return Content(site.Name);
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
