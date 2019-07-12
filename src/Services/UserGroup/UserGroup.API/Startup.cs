using System;
using System.Linq;
using System.Reflection;
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UserGroupDbContext>(options =>
            {
                options.UseNpgsql(connectionString, sql =>
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
                })
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            services.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IGroupRepository, GroupRepository>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IGroupService, GroupService>()
                .AddScoped<AccountCreatedIntegrationEventHandler>();

            services.AddCap(options =>
            {
                options.UseEntityFramework<UserGroupDbContext>()
                    .UseRabbitMQ("rabbitmq")
                    .UseDashboard();
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
                //RegisterZipkinTracer(loggerFactory, lifetime);
            });

            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserGroup API V1");
               });
            //app.UseTracing("usergroup_api");
        }

        /// <summary>
        /// 注册Consul服务
        /// </summary>
        private void RegisterConsulService(IApplicationBuilder app,
            IApplicationLifetime lifetime,
            IOptions<ServiceDiscoveryOptions> options,
            IConsulClient consul)
        {
            var address = Configuration.GetValue<string>("ServiceRegisterUrl") ??
                throw new ArgumentNullException("ServiceRegisterUrl");
            var uri = new Uri(address);

            var serviceId = $"{options.Value.ServiceName}_{uri.Host}:{uri.Port}";
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
                Name = options.Value.ServiceName,
                Port = uri.Port
            };
            consul.Agent.ServiceRegister(registration).GetAwaiter().GetResult();
            lifetime.ApplicationStopping.Register(() =>
            {
                consul.Agent.ServiceDeregister(serviceId).GetAwaiter().GetResult();
            });
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
