using Autofac;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using NSwag.AspNetCore;
using Together.Activity.API;
using Together.Activity.API.Tasks;
using Together.Activity.Application.Commands;
using Together.Activity.Application.Elasticsearch;
using Together.Activity.Application.Queries;
using Together.Activity.Application.Validations;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Activity.Domain.AggregatesModel.CatalogAggregate;
using Together.Activity.Domain.AggregatesModel.CollectionAggregate;
using Together.Activity.Infrastructure.Data;
using Together.Activity.Infrastructure.Repositories;
using Together.BuildingBlocks.Infrastructure;
using Together.BuildingBlocks.Infrastructure.Filters;

namespace Activity.API
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
            IdentityModelEventSource.ShowPII = true;

            services.AddHostedService<AutoChangeActivityStatusTask>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddCustomAuth(Configuration)
                .AddIdentityServices()
                .AddOpenApiDocument(document => document.AddCustomSecurity(Configuration))
                .AddDbContext<ActivityDbContext>(options => options.UseMySql(Configuration.GetConnectionString("Default")))
                .AddControllers(options => options.Filters.Add<HttpGlobalExceptionFilter>())
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            ConfigureCapServices(services);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterMediatorModule(typeof(CreateActivityCommandHandler), typeof(CreateActivityCommandValidator));
            builder.RegisterGeneric(typeof(IdentifiedCommandHandler<,>)).As(typeof(IRequestHandler<,>));

            builder.RegisterType<ActivityIndexService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ActivityRepository>()
                .As<IActivityRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CategoryRepository>()
                .As<ICategoryRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CollectionRepository>()
                .As<ICollectionRepository>()
                .InstancePerLifetimeScope();

            // 查询库
            var connectionString = Configuration.GetConnectionString("Default");
            builder.Register(c => new ActivityQueries(connectionString))
                .As<IActivityQueries>()
                .InstancePerLifetimeScope();
            builder.Register(c => new CategoryQueries(connectionString))
                .As<ICategoryQueries>()
                .InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOpenApi()
               .UseSwaggerUi3(settings =>
               {
                   settings.OAuth2Client = new OAuth2ClientSettings
                   {
                       ClientId = Configuration["AzureAdB2C:SwaggerUIClientId"],
                       AppName = "Activity Swagger UI"
                   };
               });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureCapServices(IServiceCollection services)
        {
            services.AddCap(options =>
            {
                options.UseEntityFramework<ActivityDbContext>();
                options.UseRabbitMQ(options =>
                {
                    options.HostName = Configuration["CAP:RabbitMQ:HostName"];
                    options.UserName = Configuration["CAP:RabbitMQ:UserName"];
                    options.Password = Configuration["CAP:RabbitMQ:Password"];
                });
            });
        }
    }
}
