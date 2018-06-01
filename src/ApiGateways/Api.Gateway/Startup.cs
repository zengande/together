using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Api.Gateway
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationProviderKey = "together";

            services.AddAuthentication()
                .AddIdentityServerAuthentication(authenticationProviderKey, o=> {
                    o.Authority = "http://localhost:5000";
                    o.ApiName = "api_gateway";
                    o.SupportedTokens = SupportedTokens.Both;
                    o.ApiSecret = "secret";
                    o.RequireHttpsMetadata = false;
                });

            services.AddOcelot()
                .AddStoreOcelotConfigurationInConsul();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseOcelot();
        }
    }
}
