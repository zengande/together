using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public class CategoriesService
        : ICategoriesService
    {
        private readonly HttpClient _http;
        private readonly string _requestBaseUrl;
        private readonly AppSettings _settings;
        public CategoriesService(IOptions<AppSettings> options,
            HttpClient httpClient)
        {
            _http = httpClient;
            _settings = options?.Value ?? throw new ArgumentNullException(nameof(_settings));
            _requestBaseUrl = "http://localhost:54868/api/v1/categories";//$"{_settings.APIGatewayEndpoint}/api/";
        }

        public async Task<List<Category>> GetCategories(int? parentId = null)
        {
            var responseString = await _http.GetStringAsync($"{_requestBaseUrl}?parentId={parentId}");

            var categories = JsonConvert.DeserializeObject<List<Category>>(responseString);

            return categories;
        }
    }
}
