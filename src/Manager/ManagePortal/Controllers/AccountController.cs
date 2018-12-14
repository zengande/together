using ManagePortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ManagePortal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly string _identityUrl;
        private readonly HttpClient _http;
        public AccountController(IConfiguration configuration)
        {
            _http = HttpClientFactory.Create();
            _identityUrl = configuration.GetValue("IdentityUrl", "");
        }

        [HttpPost]
        public async Task<IActionResult> Token([FromBody]LoginRequestParam model)
        {
            var parame = new Dictionary<string, string>();
            parame.Add("client_id", "manage_portal");
            parame.Add("client_secret", "B8604369-C6CE-424C-9DC7-88FFDAB928AA");
            parame.Add("grant_type", "password");
            parame.Add("username", model.UserName);
            parame.Add("password", model.Password);

            var content = new FormUrlEncodedContent(parame);
            var response = await _http.PostAsync($"{_identityUrl}/connect/token", content);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = DeserializeObject<TokenResult>(json);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> UserInfo([FromHeader(Name = "Authorization")]string token)
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _http.GetAsync($"{_identityUrl}/connect/userinfo");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var userInfo = DeserializeObject<UserInfo>(json);
                var roles = userInfo?.role.Split(",", StringSplitOptions.RemoveEmptyEntries);
                if (roles?.Any(r => r.Equals(Constants.Admin, StringComparison.CurrentCultureIgnoreCase)) == true)
                {
                    return Ok(userInfo);
                }
            }
            return BadRequest();
        }

        private T DeserializeObject<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }

    public class Constants
    {
        public const string Admin = "Administrator";
    }
}