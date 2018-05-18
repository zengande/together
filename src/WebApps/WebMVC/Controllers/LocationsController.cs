using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Nutshell.Resilience.HttpRequest.abstracts;

namespace WebMVC.Controllers
{
    public class LocationsController
        : Controller
    {
        private readonly IHttpClient _httpClient;
        private readonly AppSettings _appSettings;
        public LocationsController(IHttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _appSettings = settings?.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keyword)
        {
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var response = await _httpClient.GetStringAsync($"http://restapi.amap.com/v3/place/text?key={_appSettings.GaoDeMapKey}&keywords=杭州&types=&city=&children=&offset=20&page=1&extensions=base");
                if(!string.IsNullOrWhiteSpace(response))
                {
                    //JsonConvert.DeserializeObject(response);
                    return Json(response);
                }
            }
            return BadRequest(new { success = false ,message="不能为空"});
        }
    }
}
