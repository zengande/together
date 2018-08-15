using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMVC.Services
{
    public class ActivityService
        : IActivityService
    {
        private readonly HttpClient _httpClient;
        public ActivityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetActivities(int pageIndex = 1, int pageSize = 20)
        {
            var url = $"http://localhost:5100/api/v1/activities?pageIndex={pageIndex}&pageSize={pageSize}";
            var responseString = await _httpClient.GetStringAsync(url);
            return responseString;
        }
    }
}
