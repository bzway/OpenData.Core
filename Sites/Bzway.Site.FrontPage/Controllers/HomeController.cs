﻿using Microsoft.AspNetCore.Mvc;

namespace Bzway.Site.FrontPage.Controllers
{
    public class HomeController : Controller
    {
         
        public HomeController( )
        { 
        }
        public IActionResult Index()
        {
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
