using App.Metrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Threading.Tasks;

namespace Api.Gateway
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
            var identityUrl = Configuration.GetValue<string>("IdentityUrl");
            var authenticationProviderKey = "IdentityApiKey";

            services.AddOptions();
            services.Configure<AppMetricsOptions>(Configuration.GetSection("AppMetrics"));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddAuthentication()
                .AddJwtBearer(authenticationProviderKey, x =>
                {
                    x.Authority = identityUrl;
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidAudiences = new[] { "user_group_api", "activities" }
                    };
                    x.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents()
                    {
                        OnAuthenticationFailed = ctx =>
                        {
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = ctx =>
                        {
                            return Task.CompletedTask;
                        },

                        OnMessageReceived = ctx =>
                        {
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddOcelot(Configuration);

            services.AddAppMetrics(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(ILoggerFactory loggerFactory, IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHsts();

            app.UseCors("CorsPolicy");

            bool isOpenMetrics = Convert.ToBoolean(Configuration["AppMetrics:IsOpen"]);
            if (isOpenMetrics)
            {
                app.UseMetricsAllEndpoints();
                app.UseMetricsAllMiddleware();
            }

            app.UseWebSockets();
            app.UseOcelot();
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppMetrics(this IServiceCollection services, IConfiguration configuration)
        {
            var options = services.BuildServiceProvider()
                .GetRequiredService<IOptions<AppMetricsOptions>>()?.Value;
            if (options?.IsOpen == true)
            {
                var uri = new Uri(options.ConnectionString);
                var metrics = AppMetrics.CreateDefaultBuilder().Configuration.Configure(opt =>
                {
                    opt.AddAppTag(options.App);
                    opt.AddEnvTag(options.Env);
                }).Report.ToInfluxDb(opt =>
                {
                    opt.InfluxDb.BaseUri = uri;
                    opt.InfluxDb.Database = options.DatabaseName;
                    opt.InfluxDb.UserName = options.UserName;
                    opt.InfluxDb.Password = options.Password;
                    opt.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                    opt.HttpPolicy.FailuresBeforeBackoff = 5;
                    opt.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                    opt.FlushInterval = TimeSpan.FromSeconds(5);
                }).Build();

                services.AddMetrics(metrics);
                //services.AddMetricsReportScheduler();
                services.AddMetricsTrackingMiddleware();
                services.AddMetricsEndpoints();
            }
            return services;
        }
    }

    public class AppMetricsOptions
    {
        public bool IsOpen { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string App { get; set; }
        public string Env { get; set; }
    }

}
