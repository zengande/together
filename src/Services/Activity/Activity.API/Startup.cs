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
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using Microsoft.Extensions.Logging;
using System.Text;

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
            var connectionString = Configuration.GetValue<string>("ConnectionString");

            services.AddDbContext<ActivityDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sql =>
                    sql.MigrationsAssembly(typeof(ActivityDbContext).GetTypeInfo().Assembly.GetName().Name));
            });

            services.AddOptions();
            services.Configure<ServiceDiscoveryOptions>(Configuration.GetSection("ServiceDiscovery"));

            services.AddScoped<ActivityExpiredIntegrationEventHandler>();

            services.AddCap(options =>
            {
                options.UseDashboard();
                options.UseRabbitMQ("rabbitmq");
                options.UseSqlServer(connectionString);
            });

            ConfigureAuthService(services);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddControllersAsServices();

            services.AddSwaggerGen(options =>
            {
                var identityUrl = Configuration.GetValue<string>("IdentityUrlExternal");
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info { Title = "Activity HTTP API", Version = "v1" });
                options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"{identityUrl}/connect/authorize",
                    TokenUrl = $"{identityUrl}/connect/token",
                    Scopes = new Dictionary<string, string> {
                        { "activities","Activity API"}
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

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
            ILoggerFactory loggerFactory,
            IOptions<ServiceDiscoveryOptions> serviceOptions)
        {
            lifetime.ApplicationStarted.Register(() => { RegisterConsulService(app, lifetime, serviceOptions, consul, loggerFactory); });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            ConfigureAuth(app);

            app.UseCap();

            app.UseMvc();

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Activity API V1");
                   c.OAuthClientId("activityswaggerui");
                   c.OAuthAppName("Activity Swagger UI");
               });
        }

        /// <summary>
        /// 注册Consul服务
        /// </summary>
        private void RegisterConsulService(IApplicationBuilder app,
            IApplicationLifetime lifetime,
            IOptions<ServiceDiscoveryOptions> options,
            IConsulClient consul,
            ILoggerFactory loggerFactory)
        {
            //if (app.Properties["server.Features"] is FeatureCollection features)
            //{
            //    var addresses = features.Get<IServerAddressesFeature>()
            //        .Addresses
            //        .Select(a => new Uri(a));
            //    foreach (var address in addresses)
            //    {
            //        var serviceId = $"{options.Value.ServiceName}_{address.Host}:{address.Port}";
            //        var httpCheck = new AgentServiceCheck
            //        {
            //            DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
            //            Interval = TimeSpan.FromSeconds(30),
            //            HTTP = new Uri(address, "HealthCheck").OriginalString
            //        };
            //        var registration = new AgentServiceRegistration
            //        {
            //            Checks = new[] { httpCheck },
            //            Address = address.Host,
            //            ID = serviceId,
            //            Name = options.Value.ServiceName,
            //            Port = address.Port
            //        };
            //        consul.Agent.ServiceRegister(registration).GetAwaiter().GetResult();
            //        lifetime.ApplicationStopping.Register(() =>
            //        {
            //            consul.Agent.ServiceDeregister(serviceId).GetAwaiter().GetResult();
            //        });
            //    }
            //}

            var ip = "10.0.1.46";
            loggerFactory.CreateLogger("Register Consul").LogError($"当前应用程序IP： {LocalIPAddress}");
            var serviceId = $"{options.Value.ServiceName}_{ip}:5100";
            var httpCheck = new AgentServiceCheck
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                Interval = TimeSpan.FromSeconds(30),
                HTTP = $"http://{ip}:5100/HealthCheck"
            };
            var registration = new AgentServiceRegistration
            {
                Checks = new[] { httpCheck },
                Address = ip,
                ID = serviceId,
                Name = options.Value.ServiceName,
                Port = 5100
            };
            consul.Agent.ServiceRegister(registration).GetAwaiter().GetResult();
            lifetime.ApplicationStopping.Register(() =>
            {
                consul.Agent.ServiceDeregister(serviceId).GetAwaiter().GetResult();
            });
        }

        private void ConfigureAuthService(IServiceCollection services)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //var identityUrl = Configuration.GetValue<string>("IdentityUrl");
            var identityUrl = Configuration.GetValue<string>("IdentityUrlExternal");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
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

        private string LocalIPAddress
        {
            get
            {
                UnicastIPAddressInformation mostSuitableIp = null;
                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                foreach (var network in networkInterfaces)
                {
                    if (network.OperationalStatus != OperationalStatus.Up)
                        continue;
                    var properties = network.GetIPProperties();
                    if (properties.GatewayAddresses.Count == 0)
                        continue;

                    foreach (var address in properties.UnicastAddresses)
                    {
                        if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                            continue;
                        if (IPAddress.IsLoopback(address.Address))
                            continue;
                        return address.Address.ToString();
                    }
                }
                return mostSuitableIp != null
                    ? mostSuitableIp.Address.ToString()
                    : "";
            }
        }
    }
}
