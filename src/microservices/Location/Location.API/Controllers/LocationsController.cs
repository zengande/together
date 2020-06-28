using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Location.Application.Services;

namespace Together.Location.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsService _locationsService;
        public LocationsController(ILocationsService locationsService)
        {
            _locationsService = locationsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(int? parentCode = 0)
        {
            var locations = await _locationsService.GetLocationsAsync(parentCode);
            return Ok(locations);
        }

        [HttpGet, Route("cities")]
        public async Task<IActionResult> GetCitiesAsync()
        {
            var cities = await _locationsService.GetAllCitiesAsync();
            return Ok(cities);
        }
    }
}
