using Microsoft.AspNetCore.Mvc;
using Nutshell.Common.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Notice.Services;

namespace Together.Notice.Controllers
{
    public class HomeController
        : Controller
    {
        private IEmailSender _sender;
        private ICacheService _cacheService;
        public HomeController(ICacheService cacheService, IEmailSender sender)
        {
            _sender = sender;
            _cacheService = cacheService;
        }
        public async Task<IActionResult> Index()
        {
            _cacheService.Add("username", "zengande");
            await _sender.Send("zengande@qq.com", "test", "test", "<b>test</b>");
            await _sender.Send("835290734@qq.com", "test", "<b>test</b>");
            return Content("OK");
        }
    }
}
