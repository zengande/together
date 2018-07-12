using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DnsClient;
using Together.Identity.API.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Together.Identity.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Together.Identity.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
           
            ViewData["Message"] =$"hello world!";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
