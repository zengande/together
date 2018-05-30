using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Together.UserGroup.API.Configurations;
using Together.UserGroup.API.Infrastructure.Data;
using Together.UserGroup.API.Infrastructure.Repositories;
using Together.UserGroup.API.Infrastructure.Services;
using Together.UserGroup.API.IntegrationEventHandlers;
using zipkin4net;
using zipkin4net.Middleware;
using zipkin4net.Tracers;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Transport.Http;

namespace Together.UserGroup.API
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
            var assembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<UserGroupDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"), sql =>
                    sql.MigrationsAssembly(assembly));
            });

            services.AddOptions();
            services.Configure<ServiceDiscoveryOptions>(Configuration.GetSection("ServiceDiscovery"));
            services.AddSingleton<IConsulClient>(p => new ConsulClient(cfg =>
            {
                var serviceConfiguration = p.GetRequiredService<IOptions<ServiceDiscoveryOptions>>().Value;

                if (!string.IsNullOrEmpty(serviceConfiguration.Consul.HttpEndpoint))
                {
                    // if not configured, the client will use the default value "127.0.0.1:8500"
                    cfg.Address = new Uri(serviceConfiguration.Consul.HttpEndpoint);
                }
            }));

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    // 忽略循环引用
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //不使用驼峰样式的key
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    //设置时间格式
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                    options.SerializerSettings.MaxDepth = 2;
                });

            services.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IGroupRepository, GroupRepository>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IGroupService, GroupService>()
                .AddScoped<AccountCreatedIntegrationEventHandler>();

            services.AddCap(options =>
            {
                options.UseEntityFramework<UserGroupDbContext>()
                    .UseRabbitMQ(r =>
                    {
                        r.HostName = "localhost";
                        r.Port = 32771;
                    })
                    .UseDashboard()
                    .UseDiscovery(d =>
                    {
                        d.DiscoveryServerHostName = "localhost";
                        d.DiscoveryServerPort = 8500;
                        d.CurrentNodeHostName = "localhost";
                        d.CurrentNodePort = 58750;
                        d.NodeName = "User Group Api Cap No.1 Node";
                        d.NodeId = 1;
                    });
            });

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info { Title = "UserGroup API", Version = "v1" });

                //options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                //{
                //    Type = "oauth2",
                //    Flow = "implicit",
                //    AuthorizationUrl = $"{Configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize",
                //    TokenUrl = $"{Configuration.GetValue<string>("IdentityUrlExternal")}/connect/token",
                //    Scopes = new Dictionary<string, string>()
                //    {
                //        { "activities", "Activity API" }
                //    }
                //});
                //options.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IApplicationLifetime lifetime,
            ILoggerFactory loggerFactory,
            IOptions<ServiceDiscoveryOptions> serviceOptions,
            IConsulClient consul)
        {
            lifetime.ApplicationStarted.Register(() =>
            {
                RegisterConsulService(app, lifetime, serviceOptions, consul);
                RegisterZipkinTracer(loggerFactory, lifetime);
            });

            app.UseCap();
            app.UseMvc();
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserGroup API V1");
               });
            app.UseTracing("usergroup_api");
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

        private static void RegisterZipkinTracer(
            ILoggerFactory logger,
            IApplicationLifetime lifetime)
        {
            TraceManager.SamplingRate = 1.0f;
            var _logger = new TracingLogger(logger, "zipkin4net");
            var httpSender = new HttpZipkinSender("http://localhost:9411", "application/json");
            var tracer = new ZipkinTracer(httpSender, new JSONSpanSerializer(), new Statistics());

            var consoleTracer = new ConsoleTracer();

            TraceManager.RegisterTracer(consoleTracer);
            TraceManager.RegisterTracer(tracer);
            TraceManager.Start(_logger);
            lifetime.ApplicationStopped.Register(() =>
            {
                TraceManager.Stop();
            });
        }
    }
}
