using IdentityServer4;
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
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess
                }
            },
            new Client
                {
                    ClientId = "together_spa",
                    ClientName = "React SPA Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RedirectUris = { "http://localhost:3000/account/callback" },
                    PostLogoutRedirectUris = { "http://localhost:3000/logout" },
                    AllowedCorsOrigins = { "http://localhost:3000" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                },
            new Client
                {
                    ClientId = "activityswaggerui",
                    ClientName = "Activity Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent=false,
                    RedirectUris = { $"http://localhost:5100/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"http://localhost:5100/swagger/" },

                    AllowedScopes =
                    {
                        "activities"
                    }
                },
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
            new ApiResource("user_group_api", "User Group Service"),
            new ApiResource("activities", "Activity Service")
        };

        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
}
