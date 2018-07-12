﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nutshell.Common.Cache;
using Together.Notice.IntegrationEventHandlers;
using Together.Notice.Services;

namespace Together.Notice
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sql =>
                    sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            });

            services.AddScoped<IEmailSender, EmailSender>()
                .AddScoped<SendEmailNoticeIntegrationEventHandler>()
                .AddScoped<IEmailTemplateService, EmailTemplateService>()
                .AddSingleton<ICacheService>(p => new RedisCacheService(new RedisCacheOptions
                {
                    Configuration = Configuration.GetSection("RedisConnectionString").Value,
                    InstanceName = Configuration.GetSection("RedisInstanceName").Value
                }));

            services.AddCap(x =>
            {
                x.UseEntityFramework<ApplicationDbContext>();
                x.UseDashboard();
                x.UseRabbitMQ(config =>
                {
                    config.HostName = "localhost";
                });
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseCap();
            app.UseMvcWithDefaultRoute();
        }
    }
}
