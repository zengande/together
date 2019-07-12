using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Together.Activity.API.Applications.Queries;

namespace Together.Activity.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController
        : Controller
    {
        private readonly ICategoryQueries _categoryQueries;
        public CategoriesController(ICategoryQueries categoryQueries)
        {
            _categoryQueries = categoryQueries;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int? parentId)
        {
            var categories = await _categoryQueries.GetCategoriesAsync(parentId);
            return Ok(categories);
        }
    }
}
