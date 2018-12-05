using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Reflection;
using Together.Extensions.Consul;
using Together.Searching.API.Data;
using Together.Searching.API.IntegrationEventHandlers;
using Together.Searching.API.Models;

namespace Together.Searching.API
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetValue<string>("ConnectionString");
            services.AddDbContext<SearchingDbContext>(options =>
            {
                options.UseNpgsql(connectionString, sql =>
                    sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            });

            services.AddSingleton(p =>
            {
                var connection = Configuration.GetValue("ElasticSearchUrl", "");
                var node = new Uri(connection);
                return new ElasticClient(new ConnectionSettings(node).DefaultMappingFor<Activity>(m => m.IndexName("activities")));
            })
                .AddTransient<NewActivityCreatedIntegrationEventHandler>();

            services.AddCap(options =>
            {
                options.UseEntityFramework<SearchingDbContext>()
                    .UseRabbitMQ(Configuration.GetValue<string>("RabbitmqHost"))
                    .UseDashboard();
            });

            services.Configure<ServiceRegisterOptions>(Configuration);
            services.AddConsulClient();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApplicationLifetime lifetime, IConsulClient consul, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("/healthcheck", builder => builder.Run(async ctx => ctx.Response.StatusCode = 200));

            app.RegisterConsulService(lifetime, consul, loggerFactory);
            app.UseMvc();
        }
    }
}
