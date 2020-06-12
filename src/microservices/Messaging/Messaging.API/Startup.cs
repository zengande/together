using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Messaging.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Together.Messaging.API
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
            services.AddDbContext<MessagingDbContext>(options => options.UseMySql(Configuration.GetConnectionString("Default")))
                .AddSignalR();
                //.AddAzureSignalR();

            ConfigureAuthService(services);
            ConfigureCAP(services);
            ConfigureMediatR(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationsHub>("/notificationhub", options =>
                {
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransports.All;
                });
            });
        }

        private void ConfigureAuthService(IServiceCollection services)
        {
            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.Authority = $"https://together2.b2clogin.com/{Configuration["AzureAdB2C:Tenant"]}/{Configuration["AzureAdB2C:Policy"]}/v2.0/";
                    jwtOptions.Audience = Configuration["AzureAdB2C:ClientId"];
                });
        }
        private void ConfigureCAP(IServiceCollection services)
        {
            services.AddTransient<CapSubscriberService>();

            services.AddCap(options =>
            {
                options.UseEntityFramework<MessagingDbContext>();
                options.UseRabbitMQ(options =>
                {
                    options.HostName = Configuration["CAP:RabbitMQ:HostName"];
                    options.UserName = Configuration["CAP:RabbitMQ:UserName"];
                    options.Password = Configuration["CAP:RabbitMQ:Password"];
                });
            });
        }
        private void ConfigureMediatR(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly);
        }
    }
}
