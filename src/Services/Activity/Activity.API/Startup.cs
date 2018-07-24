using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Together.Activity.Infrastructure.Data;
using MediatR;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Together.Activity.API.Infrastructure.AutofacModules;
using Swashbuckle.AspNetCore.Swagger;
using Consul;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Together.Activity.API.Applications.Filters;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Together.Activity.API.Applications.IntegrationEvents.EventHandlers;
using Together.Activity.API.Extensions;

namespace Together.Activity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ActivityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sql =>
                    sql.MigrationsAssembly(typeof(ActivityDbContext).GetTypeInfo().Assembly.GetName().Name));
            });

            services.AddOptions();
            services.Configure<ServiceDiscoveryOptions>(Configuration.GetSection("ServiceDiscovery"));

            services.AddScoped<ActivityExpiredIntegrationEventHandler>();

            services.AddCap(options =>
            {
                options.UseDashboard();
                options.UseRabbitMQ("localhost");
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddControllersAsServices();

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info { Title = "Activity HTTP API", Version = "v1" });
                options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = "http://localhost:5000/connect/authorize",
                    TokenUrl = "http://localhost:5000/connect/token",
                    Scopes = new Dictionary<string, string> {
                        { "activities","Activity API"}
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            ConfigureAuthService(services);

            services.AddMediatR(typeof(Startup));
            var container = new ContainerBuilder();
            container.Populate(services);
            var serviceConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<ServiceDiscoveryOptions>>();
            container.RegisterModule(new ApplicationModule(Configuration.GetConnectionString("DefaultConnection"),
                serviceConfiguration));
            container.RegisterModule(new MediatorModule());
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime lifetime,
            IConsulClient consul,
            IOptions<ServiceDiscoveryOptions> serviceOptions)
        {
            lifetime.ApplicationStarted.Register(() => { RegisterConsulService(app, lifetime, serviceOptions, consul); });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            ConfigureAuth(app);

            app.UseCap();

            app.UseMvc();

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Activity API V1");
               });

        }

        /// <summary>
        /// 注册Consul服务
        /// </summary>
        private void RegisterConsulService(IApplicationBuilder app,
            IApplicationLifetime lifetime,
            IOptions<ServiceDiscoveryOptions> options,
            IConsulClient consul)
        {
            if (app.Properties["server.Features"] is FeatureCollection features)
            {
                var addresses = features.Get<IServerAddressesFeature>()
                    .Addresses
                    .Select(a => new Uri(a));
                foreach (var address in addresses)
                {
                    var serviceId = $"{options.Value.ServiceName}_{address.Host}:{address.Port}";
                    var httpCheck = new AgentServiceCheck
                    {
                        DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                        Interval = TimeSpan.FromSeconds(30),
                        HTTP = new Uri(address, "HealthCheck").OriginalString
                    };
                    var registration = new AgentServiceRegistration
                    {
                        Checks = new[] { httpCheck },
                        Address = address.Host,
                        ID = serviceId,
                        Name = options.Value.ServiceName,
                        Port = address.Port
                    };
                    consul.Agent.ServiceRegister(registration).GetAwaiter().GetResult();
                    lifetime.ApplicationStopping.Register(() =>
                    {
                        consul.Agent.ServiceDeregister(serviceId).GetAwaiter().GetResult();
                    });
                }
            }
        }

        private void ConfigureAuthService(IServiceCollection services)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var identityUrl = "http://localhost:5000";//Configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "activities";
            });
        }

        protected virtual void ConfigureAuth(IApplicationBuilder app)
        {
            //if (Configuration.GetValue<bool>("UseLoadTest"))
            //{
            //    app.UseMiddleware<ByPassAuthMiddleware>();
            //}

            app.UseAuthentication();
        }
    }
}
