using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Topic.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class TopicController
        : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery]string topic)
        {
            return Ok(Models.Topic.MockList().Where(t => t.Id.Equals(topic, StringComparison.CurrentCultureIgnoreCase)));
        }
    }
}
