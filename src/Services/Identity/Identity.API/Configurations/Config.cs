using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Together.Identity.API.Configurations
{
    public class Config
    {
        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client{
                ClientId = "mvc",
                ClientName = "MVC Client",
                ClientSecrets = new List<Secret>{
                    new Secret("secret".Sha256())
                },
                RequireConsent = false,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AllowOfflineAccess = true,
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Implicit,
                RedirectUris = { "http://localhost:5001/signin-oidc" },
                PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },
                AllowedScopes = {
                    "api_gateway",
                    IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                    IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess
                }
            }
        };

        public static List<TestUser> GetUsers() => new List<TestUser>
        {
            new TestUser{
                SubjectId = "10000",
                Username = "zengande",
                Password="pass@word"
            }
        };

        public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource>
        {
            new ApiResource("user_group_api", "User Group Service")
        };

        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
}
