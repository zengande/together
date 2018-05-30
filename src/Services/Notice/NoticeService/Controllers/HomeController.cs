using Microsoft.AspNetCore.Mvc;
using Nutshell.Common.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Notice.Controllers
{
    public class HomeController
        : Controller
    {
        private ICacheService _cacheService;
        public HomeController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public IActionResult Index()
        {
            _cacheService.Add("username", "zengande");
            return Content("OK");
        }
    }
}
