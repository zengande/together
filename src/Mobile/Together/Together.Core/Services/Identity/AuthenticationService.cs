using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Together.Core.Models.Identity;

namespace Together.Core.Services.Identity
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string CreateAuthorizationRequest()
        {
            throw new NotImplementedException();
        }

        public string CreateLogoutRequest(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<UserToken> GetTokenAsync(string username, string password)
        {
            var discovery = await _httpClient.GetDiscoveryDocumentAsync();
            if (discovery.IsError == false)
            {
                var request = new PasswordTokenRequest
                {
                    Address = $"{GlobalSettings.IdentityEndpoint}/{discovery.TokenEndpoint}",
                    UserName = username,
                    Password = password,
                    ClientId = GlobalSettings.ClientId,
                    ClientSecret = GlobalSettings.ClientSecret,
                    Scope = GlobalSettings.Scope
                };
                var response = await _httpClient.RequestPasswordTokenAsync(request);
                if (response.IsError == false)
                {
                    return new UserToken
                    {
                        RefreshToken = response.RefreshToken,
                        AccessToken = response.AccessToken,
                        ExpiresIn = response.ExpiresIn,
                        IdToken = response.IdentityToken,
                        TokenType = response.TokenType
                    };
                }
            }
            return null;
        }

        public Task<bool> IsAuthenticated()
        {
            return Task.FromResult(false);
        }
    }
}
