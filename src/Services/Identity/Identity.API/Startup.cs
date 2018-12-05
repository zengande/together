using DnsClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Reflection;
using Together.Identity.API.Configurations;
using Together.Identity.API.Data;
using Together.Identity.API.Models;
using Together.Identity.API.Services;
using zipkin4net;
using zipkin4net.Middleware;
using zipkin4net.Tracers;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Transport.Http;

namespace Together.Identity.API
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("ConnectionString");
            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseNpgsql(connectionString,
                    sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            });

            //services.AddDataProtection()
            //    .PersistKeysToRedis(ConnectionMultiplexer.Connect(configuration: "nosql.redis.data"), "DataProtection-Key")
            //    //.PersistKeysToRedis(ConnectionMultiplexer.Connect(configuration: "localhost:6379"), "DataProtection-Key")
            //    .SetApplicationName("IdentityAPI");
            //services.AddDistributedRedisCache(options =>
            //{
            //    options.Configuration = "nosql.redis.data";
            //    //options.Configuration = "localhost:6379";
            //    options.InstanceName = "DataProtection";
            //});
            //services.AddSession();
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.Name = ".AspNet.SharedCookie";
            //});

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            var clients = new Dictionary<string, string>
            {
                { "mvc", Configuration.GetValue("MvcClientUrl", "") },
                { "manage_portal", Configuration.GetValue("ManagePortalUrl", "") },
                { "notice_dashboard", Configuration.GetValue("NoticeDashboard", "") },
                { "activity_api", Configuration.GetValue("ActivityApiUrl", "") }
            };
            services.AddIdentityServer(x =>
            {
                x.IssuerUri = "null";
                x.Authentication.CookieLifetime = TimeSpan.FromHours(2);
            })
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients(clients))
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<ProfileService>();

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddCap(options =>
            {
                options.UseEntityFramework<IdentityDbContext>()
                    .UseRabbitMQ("rabbitmq")
                    .UseDashboard();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("ManagePortalCors", policy =>
                {
                    policy.WithOrigins("http://localhost:8000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            services.AddMvc()
               .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IUserService, UserService>()
                .AddSingleton<IDnsQuery>(p =>
                {
                    var options = p.GetRequiredService<IOptions<ServiceDiscoveryOptions>>().Value;
                    return new LookupClient(options.ConsulDnsEndpoint.ToIPEndPoint());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IApplicationLifetime lifetime)
        {
            logger.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // RegisterZipkinTracer(app, logger, lifetime);

            //app.UseSession();
            app.UseCors("ManagePortalCors");
            app.UseIdentityServer();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void RegisterZipkinTracer(IApplicationBuilder app,
            ILoggerFactory logger,
            IApplicationLifetime lifetime)
        {
            lifetime.ApplicationStarted.Register(() =>
            {
                TraceManager.SamplingRate = 1.0f;
                var _logger = new TracingLogger(logger, "zipkin4net");
                var httpSender = new HttpZipkinSender("http://localhost:9411", "application/json");
                var tracer = new ZipkinTracer(httpSender, new JSONSpanSerializer(), new Statistics());

                var consoleTracer = new ConsoleTracer();

                TraceManager.RegisterTracer(consoleTracer);
                TraceManager.RegisterTracer(tracer);
                TraceManager.Start(_logger);
            });
            lifetime.ApplicationStopped.Register(() =>
            {
                TraceManager.Stop();
            });
            app.UseTracing("identity_api");
        }
    }
}
