using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Location.API.Data;
using Location.API.Infrastructure.Repositories;
using Location.API.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Location.API
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
            services.Configure<LocationsSettings>(Configuration.GetSection("LocationsSettings"));

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<ILocationService, LocationService>()
                .AddScoped<ILocationRepository, LocationRepository>()
                .AddSingleton<IConsulClient>(p =>
                {
                    return new ConsulClient(new Action<ConsulClientConfiguration>(cfg =>
                    {
                        // if not configured, the client will use the default value "127.0.0.1:8500"
                        cfg.Address = new Uri(Configuration.GetValue<string>("ConsulHttpEndpoint"));
                    }));
                });

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info { Title = "Location API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IApplicationLifetime lifetime,
            IConsulClient consul,
            IHostingEnvironment env,
            ILoggerFactory logger)
        {
            lifetime.ApplicationStarted.Register(() => { RegisterConsulService(app, lifetime, consul, logger); });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "");
            });
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Location API V1");
               });

            LocationsContextSeed.SeedAsync(app, logger).Wait();
        }

        /// <summary>
        /// 注册/卸载Consul服务
        /// </summary>
        private void RegisterConsulService(IApplicationBuilder app,
            IApplicationLifetime lifetime,
            IConsulClient consul,
            ILoggerFactory loggerFactory)
        {
            var address = Configuration.GetValue<string>("ServiceRegisterUrl") ??
                throw new ArgumentNullException("ServiceRegisterUrl");
            var uri = new Uri(address);
            var serviceName = Configuration.GetValue<string>("ServiceName");

            var serviceId = $"{serviceName}_{uri.Host}:{uri.Port}";
            var httpCheck = new AgentServiceCheck
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                Interval = TimeSpan.FromSeconds(30),
                HTTP = Configuration.GetValue<string>("HealthCheckUrl")
            };
            var registration = new AgentServiceRegistration
            {
                Checks = new[] { httpCheck },
                Address = uri.Host,
                ID = serviceId,
                Name = serviceName,
                Port = uri.Port
            };
            consul.Agent.ServiceRegister(registration).GetAwaiter().GetResult();
            lifetime.ApplicationStopping.Register(() =>
            {
                consul.Agent.ServiceDeregister(serviceId).GetAwaiter().GetResult();
            });
        }
    }
}
