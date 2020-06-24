using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Activity.Application.Elasticsearch;

namespace Together.Activity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ActivityIndexService _indexService;
        public SearchController(ActivityIndexService indexService)
        {
            _indexService = indexService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string q)
        {
            var result = await _indexService.Search(q);
            return Ok(result);
        }
    }
}
