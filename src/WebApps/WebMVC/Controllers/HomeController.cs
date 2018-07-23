using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Nutshell.Resilience.HttpRequest.abstracts;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHttpClient _httpClient;
        private readonly IOptions<AppSettings> _appSettings;

        public HomeController(IHttpClient httpClient, 
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> appSettings)
            : base(httpContextAccessor)
        {
            _appSettings = appSettings;
            _httpClient = httpClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
