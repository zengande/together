using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using Refit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using WebMVC.Infrastructure.API;
using WebMVC.Infrastructure.Attributes;
using WebMVC.Infrastructure.HttpClientHandlers;
using WebMVC.Services;

namespace WebMVC
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

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"))
                .AddCustomAuth(Configuration)
                .AddAddLocalizations()
                .AddCustomMvc(Configuration)
                .AddRefitApiServices(Configuration)
                .AddServices(Configuration)
                .AddHttpClientLogging(Configuration)
                .AddAutoMapper()
                .AddSignalR();

            return services.BuildAspectInjectorProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseHsts();
            }

            app.UseApiRequestExceptionHandling();

            app.UseAuthentication();

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();

            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOptions.Value);

            app.UseMvcWithDefaultRoute();
        }


    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAddLocalizations(this IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(opts => SetLocalizationOptions(opts));

            services.AddLocalization(opts => opts.ResourcesPath = "Resources");
            return services;
        }

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => false;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return services;
        }

        public static IServiceCollection AddCustomAuth(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; ;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie()
                .AddOpenIdConnect(options =>
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    //options.SignedOutRedirectUri = callBackUrl.ToString();
                    options.Authority = configuration.GetValue<string>("IdentityUrl");
                    options.RequireHttpsMetadata = false;

                    options.ClientId = "mvc";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code id_token";

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.RequireHttpsMetadata = false;

                    options.Scope.Add("profile");
                    options.Scope.Add("openid");
                    options.Scope.Add("activities");
                    options.Scope.Add("user_group_api");
                    options.Scope.Add("noticeservice");
                });

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IActivityService, ActivityService>();

            return services;
        }

        public static IServiceCollection AddHttpClientLogging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(b =>
            {
                b.AddFilter((category, level) => true); // Spam the world with logs.

                // Add console logger so we can see all the logging produced by the client by default.
                b.AddConsole(c => c.IncludeScopes = true);

                // Add console logger
                b.AddDebug();
            });

            return services;
        }

        public static IServiceCollection AddRefitApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddTransient<HttpClientRequestIdDelegatingHandler>();

            var baseUrl = configuration.GetValue<string>("APIGatewayEndpoint");
            services.AddRefitClient<IActivityAPI>()
                .ConfigureHttpClient((options) =>
                {
                    options.BaseAddress = new Uri(baseUrl);
                })
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddHttpMessageHandler<HttpClientRequestIdDelegatingHandler>()
                //Steeltoe discovery
                //.AddHttpMessageHandler<DiscoveryHttpMessageHandler>()
                ;

            services.AddRefitClient<ICategoryAPI>()
                .ConfigureHttpClient(options =>
                {
                    options.BaseAddress = new Uri(baseUrl);
                })
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            return services;
        }

        public static IServiceCollection AddHttps(this IServiceCollection services)
        {
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5002;
            });

            return services;
        }

        private static void SetLocalizationOptions(RequestLocalizationOptions options)
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("zh-CN"),
                new CultureInfo("en-US")
            };

            options.DefaultRequestCulture = new RequestCulture(supportedCultures[0], supportedCultures[0]);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        }


        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
              .HandleTransientHttpError()
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
              .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}
