using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Together.Location.Application.BaiduMap;

namespace Together.BuildingBlogs.BaiDuMap.Services
{
    public class BaiduMapService : IBaiduMapService
    {
        private readonly ILogger<BaiduMapService> _logger;
        private readonly string _ak;
        private readonly HttpClient _http;
        public BaiduMapService(HttpClient http, IConfiguration configuration, ILogger<BaiduMapService> logger)
        {
            _logger = logger;
            _http = http;
            _ak = configuration["BaiduMap:AK"];
        }

        public async Task<ReverseGeoCodingResult> ReverseGeoCodingAsync(string location)
        {
            var url = $"reverse_geocoding/v3/?ak={_ak}&output=json&coordtype=bd09ll&location={location}";
            try
            {
                var response = await _http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ReverseGeoCodingResult>(content);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return default;
        }
    }
}
