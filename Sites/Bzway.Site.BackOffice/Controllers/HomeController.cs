using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bzway.Module.Wechat.Interface;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Bzway.Site.BackOffice.Controllers
{
    public class HomeController : Controller
    {
        IWechatApiService wechatApiService;
        public HomeController(IWechatApiService wechatApiService)
        {
            this.wechatApiService = wechatApiService;

        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var result = this.wechatApiService.GetMaterialList("news", 0, 1);


            return View();
        }
    }
}
