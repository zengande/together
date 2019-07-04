using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace WebMVC.Controllers
{
    public class LocationsController
        : Controller
    {
       

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            return Ok("");
        }
    }
}
