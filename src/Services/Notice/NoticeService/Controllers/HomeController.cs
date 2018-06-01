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
        private IEmailTemplateService _templateService;
        public HomeController(ICacheService cacheService, IEmailSender sender, IEmailTemplateService emailTemplateService)
        {
            _templateService = emailTemplateService;
            _sender = sender;
            _cacheService = cacheService;
        }
        public async Task<IActionResult> Index()
        {
            var template = await _templateService.GetTemplate(1);
            if (template == null)
            {
                return Content("NULL");
            }
            var values = new Dictionary<string, string>();
            values.Add("link", "zengande.cn");
            var content = _templateService.Build(template.Template, values);
            await _sender.Send("835290734@qq.com", template.Title, content);
            return Content("OK");
        }
    }
}
