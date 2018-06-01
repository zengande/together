using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Identity.API.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DnsClient;
using Microsoft.Extensions.Options;
using Together.Identity.API.Services;
using Together.Identity.API.Models;
using Nutshell.Resilience.HttpRequest.abstracts;
using Nutshell.Resilience.HttpRequest;
using Together.Identity.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using zipkin4net;
using Microsoft.AspNetCore.Hosting.Internal;
using zipkin4net.Middleware;
using zipkin4net.Transport.Http;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Tracers;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"),
                    sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            });

            services.AddDataProtection()
                .PersistKeysToRedis(ConnectionMultiplexer.Connect(configuration: "localhost:6379"), "DataProtection-Key")
                .SetApplicationName("IdentityAPI");
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
                options.InstanceName = "DataProtection";
            });
            services.AddSession();
            services.ConfigureApplicationCookie(options => {
                options.Cookie.Name = ".AspNet.SharedCookie";
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<ApplicationUser>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCap(options =>
            {
                options.UseEntityFramework<IdentityDbContext>()
                    .UseRabbitMQ(r =>
                    {
                        r.HostName = "localhost";
                        // 5672端口
                        //r.Port = 32771;
                    })
                    .UseDashboard()
                    .UseDiscovery(d =>
                    {
                        d.DiscoveryServerHostName = "localhost";
                        d.DiscoveryServerPort = 8500;
                        d.CurrentNodeHostName = "localhost";
                        d.CurrentNodePort = 5000;
                        d.NodeName = "Idnetity Api Cap No.1 Node";
                        d.NodeId = 2;
                    });
            });

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

            RegisterZipkinTracer(app, logger, lifetime);

            app.UseSession();

            app.UseIdentityServer();
            app.UseCap();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
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
