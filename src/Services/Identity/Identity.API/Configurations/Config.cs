using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace Together.Identity.API.Configurations
{
    public class Config
    {
        public static IEnumerable<Client> GetClients(Dictionary<string, string> clients) => new List<Client>
        {
            new Client{
                ClientId = "mvc",
                ClientName = "MVC Client",
                ClientSecrets = new List<Secret>{
                    new Secret("secret".Sha256())
                },
                AllowAccessTokensViaBrowser = false,
                RequireConsent = false,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AllowOfflineAccess = true,
                AlwaysIncludeUserClaimsInIdToken=true,
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Hybrid,
                RedirectUris = { $"{clients["mvc"]}/signin-oidc" },
                PostLogoutRedirectUris = { $"{clients["mvc"]}/signout-callback-oidc" },
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "activities",
                    "user_group_api",
                    "noticeservice"
                }
            },
            new Client{
                ClientId = "manage_portal",
                ClientName = "Manage Portal SPA",
                ClientSecrets = new List<Secret>{
                    new Secret("B8604369-C6CE-424C-9DC7-88FFDAB928AA".Sha256())
                },
                AllowAccessTokensViaBrowser = true,
                RequireConsent = false,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AllowOfflineAccess = true,
                AlwaysIncludeUserClaimsInIdToken=true,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "activities",
                    "user_group_api"
                }
            },
            new Client
                {
                    ClientId = "activityswaggerui",
                    ClientName = "Activity Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent=false,
                    RedirectUris = { $"{clients["activity_api"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clients["activity_api"]}/swagger/" },

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
            new ApiResource("activities", "Activity Service"),
            new ApiResource("noticeservice", "Notification Service")
        };

        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
}
