using Nutshell.Resilience.HttpRequest.abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Identity.API.Services
{
    public class UserService
        : IUserService
    {
        private readonly string UserServiceBaseUrl = "http://localhost:58750";
        private readonly IHttpClient _httpClient;
        public UserService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async void Register(string nickname, string email, string password)
        {
            var response = await _httpClient.PostAsync($"{UserServiceBaseUrl}/api/v1/Users", new { Nickname = nickname, Email = email, Password = password });
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
