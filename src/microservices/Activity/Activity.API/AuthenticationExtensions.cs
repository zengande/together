using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Activity.API
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddCustomAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.Authority = $"https://together2.b2clogin.com/{configuration["AzureAdB2C:Tenant"]}/{configuration["AzureAdB2C:Policy"]}/v2.0/";
                    jwtOptions.Audience = configuration["AzureAdB2C:ClientId"];
                });

            return services;
        }

        public static AspNetCoreOpenApiDocumentGeneratorSettings AddCustomSecurity(this AspNetCoreOpenApiDocumentGeneratorSettings settings, IConfiguration configuration)
        {
            settings.AddSecurity("oauth2", new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.OAuth2,
                Description = "AAD B2C Authentication",
                Flow = OpenApiOAuth2Flow.Implicit,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        TokenUrl = $"https://together2.b2clogin.com/{configuration["AzureAdB2C:Tenant"]}/{configuration["AzureAdB2C:Policy"]}/oauth2/v2.0/token",
                        AuthorizationUrl = $"https://together2.b2clogin.com/{configuration["AzureAdB2C:Tenant"]}/{configuration["AzureAdB2C:Policy"]}/oauth2/v2.0/authorize",
                        Scopes = new Dictionary<string, string>
                            {
                                { "https://together2.onmicrosoft.com/activityapi/request","activity api" },
                                { "https://together2.onmicrosoft.com/activityapi/user_impersonation", "Access the api as the signed-in user"}
                            }
                    }
                }
            });

            settings.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("oauth2"));

            return settings;
        }
    }
}
