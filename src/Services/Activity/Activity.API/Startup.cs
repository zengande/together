using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Consul;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Together.Activity.API.Applications.Filters;
using Together.Activity.API.Applications.IntegrationEvents.EventHandlers;
using Together.Activity.API.Infrastructure.AutofacModules;
using Together.Activity.Infrastructure.Data;

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
                options.UseNpgsql(connectionString, sql =>
                    sql.MigrationsAssembly(typeof(ActivityDbContext).GetTypeInfo().Assembly.GetName().Name));
            });

            services.AddOptions();
            services.Configure<ServiceDiscoveryOptions>(Configuration.GetSection("ServiceDiscovery"));

            services.AddScoped<ActivityExpiredIntegrationEventHandler>();
            services.AddScoped<ActivityRecruitmentCompletedIntegrationEventHandler>();

            services.AddCap(options =>
            {
                options.UseEntityFramework<ActivityDbContext>()
                    .UseDashboard()
                    .UseRabbitMQ("rabbitmq");
            });

            ConfigureAuthService(services);

            services.AddAutoMapper();

            services.AddMvc(options =>
            {
                //options.Filters.Add<ActionFilter>()
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddControllersAsServices();

            services.AddSwaggerGen(options =>
            {
                var identityUrl = Configuration.GetValue<string>("IdentityUrl");
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

            var container = new ContainerBuilder();
            container.Populate(services);
            var serviceConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<ServiceDiscoveryOptions>>();
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule(connectionString, serviceConfiguration));
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

        private void ConfigureAuthService(IServiceCollection services)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var identityUrl = Configuration.GetValue<string>("IdentityUrl");

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
    }
}
