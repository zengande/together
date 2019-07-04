using Consul;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nutshell.Common.Cache;
using Swashbuckle.AspNetCore.Swagger;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Together.Attributes.ActionFilters;
using Together.Extensions.Consul;
using Together.Notice.Hubs;
using Together.Notice.IntegrationEventHandlers;
using Together.Notice.Services;
using Together.Notice.Tasks;

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
            services.Configure<ServiceRegisterOptions>(Configuration);
            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), sql =>
                    sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddSingleton<IRedisCacheService>(p =>
            {
                var options = new RedisCacheOptions
                {
                    Configuration = Configuration.GetValue<string>("RedisConnectionString"),
                    InstanceName = Configuration.GetValue<string>("RedisInstanceName")
                };

                return new RedisCacheService(options);
            })
                .AddSingleton<IEmailSender, EmailSender>()
                .AddScoped<SendEmailNoticeIntegrationEventHandler>()
                .AddScoped<NewUserJoinedActivityEventHandler>()
                .AddScoped<IEmailTemplateService, EmailTemplateService>()
                .AddScoped<INoticeRecordService, NoticeRecordService>()
                .AddScoped<IStatisticsService, StatisticsService>()
                .AddHostedService<SaveNoticeRecordsTask>();

            services.AddCap(x =>
            {
                x.UseEntityFramework<ApplicationDbContext>()
                    .UseRabbitMQ("rabbitmq")
                    //.UseRabbitMQ("172.22.46.136")
                    .UseDashboard()
                    .FailedRetryCount = 0;
            });

            services.AddSignalR();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    options.Authority = Configuration.GetValue<string>("IdentityUrl");
                    options.RequireHttpsMetadata = false;
                    options.Audience = "noticeservice";
                });

            services.AddSwaggerGen(options =>
            {
                var identityUrl = Configuration.GetValue<string>("IdentityUrl");
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info { Title = "Notice HTTP API", Version = "v1" });

                //options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            services.AddConsulClient();

            services.AddMvc(options =>
            {
                options.Filters.Add<ValidateModelAttribute>();
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddControllersAsServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, Microsoft.AspNetCore.Hosting.IApplicationLifetime lifetime, IConsulClient consulClient, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.RegisterConsulService(lifetime, consulClient, loggerFactory);

            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationsHub>("/notificationhub", options =>
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransports.All);
            });

            app.Map("/HealthCheck", lapp => lapp.Run(async ctx => ctx.Response.StatusCode = 200));

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Dashboard}/{action=Index}/{id?}");
            });

            app.UseSwagger()
              .UseSwaggerUI(c =>
              {
                  c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notice API V1");
                  c.OAuthAppName("Notice Swagger UI");
              });
        }
    }
}
