using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ManagePortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagePortal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly HttpClient _http;
        public AccountController()
        {
            _http = HttpClientFactory.Create();
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
            var response = await _http.PostAsync("http://localhost:5000/connect/token", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                // todo : has role admin
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> UserInfo([FromHeader(Name = "Authorization")]string token)
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _http.GetAsync("http://localhost:5000/connect/userinfo");
            if (response.IsSuccessStatusCode)
            {
                var userinfo = await response.Content.ReadAsStringAsync();
                return Ok(userinfo);
            }
            return BadRequest();
        }
    }
}