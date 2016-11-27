using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bzway.Module.Wechat.Interface;
using Microsoft.Extensions.Logging;
using System.Threading;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Bzway.Site.BackOffice.Controllers
{
    public class HomeController : Controller
    {
        #region ctor
        private readonly IWechatService wechatService;
        protected readonly ILogger logger;

        public HomeController(ILoggerFactory loggerFactory,
            IWechatService wechatService)
        {
            this.logger = loggerFactory.CreateLogger<HomeController>();
            this.wechatService = wechatService;
        }
        #endregion
        // GET: /<controller>/
        public IActionResult Index()
        {
            //this.wechatService.SyncMaterial();
            var list = this.wechatService.GetWechatMaterils();
            return View(list);
        }
    }
}
