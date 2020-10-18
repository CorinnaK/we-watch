﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using we_watch.Models;

namespace we_watch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

     /*
      
      ***** Research and add here how to check if someone is logged in or not  --- variable bool isLoggedIn 
      
      */

        public IActionResult Index()
        {
            return (isLoggedIn() ?  Redirect("/User/Login") :  Redirect("/ShowCard"));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public bool isLoggedIn()
        {
            return (HttpContext.Session.GetString("isLoggedIn") == "true");
        }
    }
}
