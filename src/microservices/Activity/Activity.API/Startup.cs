using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;
using Together.Activity.API;
using Together.Activity.Infrastructure.Data;
using Together.BuildingBlocks.Infrastructure;

namespace Activity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            services.AddCustomAuth(Configuration);

            services.AddOpenApiDocument(document => document.AddCustomSecurity(Configuration));

            services.AddCustomMediatR(typeof(Startup));

            services.AddDbContext<ActivityDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Default"));
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOpenApi()
               .UseSwaggerUi3(settings =>
               {
                   settings.OAuth2Client = new OAuth2ClientSettings
                   {
                       ClientId = Configuration["AzureAdB2C:SwaggerUIClientId"],
                       AppName = "Activity Swagger UI"
                   };
               });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
