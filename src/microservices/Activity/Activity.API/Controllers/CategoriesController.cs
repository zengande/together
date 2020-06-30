using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Activity.Application.Queries;

namespace Together.Activity.API.Controllers
{
    [OpenApiTag("活动类型")]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryQueries _queries;
        public CategoriesController(ICategoryQueries queries)
        {
            _queries = queries;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var categories = await _queries.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}
