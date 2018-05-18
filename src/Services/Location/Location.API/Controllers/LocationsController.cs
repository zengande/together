using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Location.API.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Location.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class LocationsController : Controller
    {
        private readonly ILocationService _locationService;
        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [Route("search")]
        [HttpGet]
        [ProducesResponseType(typeof(List<LocationSearchResult>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest("param 'keyword' cloud not be null or empty.");
            }
            var locations = await _locationService.Search(keyword);
            var result = new List<LocationSearchResult>();
            if (locations != null)
            {
                var address = new StringBuilder();
                foreach (var location in locations)
                {
                    if (!string.IsNullOrEmpty(location.ParentCode))
                    {
                        var parent = await _locationService.GetAsync(location.ParentCode);
                        if (parent != null)
                        {
                            address.Append($"{parent.CityName},");
                        }
                    }
                    address.Append(location.CityName);
                    result.Add(new LocationSearchResult { LocationCode = location.LocationCode, Address = address.ToString() });
                    address.Clear();
                }
            }
            return Ok(result);
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string parentCode)
        {
            var locations = await _locationService.GetCityListByParentId(parentCode);
            return Ok(locations);
        }
    }
    class LocationSearchResult
    {
        public string LocationCode { get; set; }
        public string Address { get; set; }
    }
}
