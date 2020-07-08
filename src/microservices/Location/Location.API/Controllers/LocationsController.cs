using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.BuildingBlogs.BaiDuMap.Services;
using Together.Location.Application.Dto;
using Together.Location.Application.Services;

namespace Together.Location.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly IBaiduMapService _baiduMap;
        private readonly ILocationsService _locationsService;
        public LocationsController(ILocationsService locationsService,
            IBaiduMapService baiduMap)
        {
            _baiduMap = baiduMap;
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

        [HttpGet, Route("reverse_geocoding")]
        public async Task<IActionResult> ReverseGeoCodingAsync(string location)
        {
            var result = await _baiduMap.ReverseGeoCodingAsync(location);
            if(result?.status == 0)
            {
                var userLocation = new UserLocationDto
                {
                    Lat = result.result.location.lat,
                    Lng = result.result.location.lng,
                    Province = result.result.addressComponent.province,
                    City = result.result.addressComponent.city,
                    CityCode = Convert.ToInt32(result.result.addressComponent.adcode)
                };
                return Ok(userLocation);
            }
            return BadRequest();
        }
    }
}
