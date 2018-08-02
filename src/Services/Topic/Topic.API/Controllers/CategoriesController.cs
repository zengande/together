using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Topic.API.Models;

namespace Together.Topic.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CategoriesController
        : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery]int? parentId)
        {
            if (parentId == null)
            {
                return Ok(Category.MockList().Where(c => c.ParentId == null));
            }
            return Ok(Category.MockList().Where(c => c.ParentId == parentId).OrderBy(c => c.Sort));
        }
    }
}
