using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Nutshell.BaiDuMap.API.Abstracts;
using Nutshell.BaiDuMap.API.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nutshell.BaiDuMap.API
{
    public class BaiDuMapApiClient
        : IBaiDuMapApiClient
    {
        private readonly string BaseUrl = "http://api.map.baidu.com";
        private readonly BaiDuMapOptions _options;
        private readonly HttpClient _http;
        public BaiDuMapApiClient(IOptions<BaiDuMapOptions> options,
            HttpClient http)
        {
            _options = options?.Value ??
                throw new ArgumentNullException(nameof(options));
            _http = http;
        }

        /// <summary>
        /// 地点检索
        /// </summary>
        public async Task<BaiduMapApiResult<PlaceSearchResultItem>> PlaceSearch(string query, string region = "中国")
        {
            var url = $"{BaseUrl}/place/v2/search?query={query}&region={region}&output=json&ak={_options.Ak}";
            var responseString = await _http.GetStringAsync(url);
            try
            {
                var response = JsonConvert.DeserializeObject<BaiduMapApiResult<PlaceSearchResultItem>>(responseString);
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<BaiduMapApiResult<PlaceSuggestionResultItem>> PlaceSuggestion(string query,string region = "中国")
        {
            var url = $"{BaseUrl}/place/v2/suggestion?query={query}&region={region}&city_limit=true&output=json&ak={_options.Ak}";
            var responseString = await _http.GetStringAsync(url);
            try
            {
                var response = JsonConvert.DeserializeObject<BaiduMapApiResult<PlaceSuggestionResultItem>>(responseString);
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
