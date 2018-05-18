using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            //var result = await _httpClient.GetStringAsync("http://localhost:52769/api/v1/activities");
            //var result = await _httpClient.GetStringAsync($"http://restapi.amap.com/v3/place/text?key={_appSettings.Value.GaoDeMapKey}&keywords=杭州&types=&city=&children=&offset=20&page=1&extensions=base");
            //ViewData["Result"] = result;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
