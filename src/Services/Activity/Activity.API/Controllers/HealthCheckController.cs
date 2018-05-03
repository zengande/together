using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Activity.API.Controllers
{
    [Route("[controller]")]
    public class HealthCheckController
        : ControllerBase
    {
        [HttpGet("")]
        [HttpHead("")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}
