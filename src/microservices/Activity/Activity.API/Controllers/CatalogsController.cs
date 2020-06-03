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
    public class CatalogsController : ControllerBase
    {
        private readonly ICatalogQueries _queries;
        public CatalogsController(ICatalogQueries queries)
        {
            _queries = queries;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(int? parentId = null)
        {
            var catalogs = await _queries.GetCatalogsAsync(parentId);
            return Ok(catalogs);
        }
    }
}
