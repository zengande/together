using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Together.Activity.BackgroundTasks.Data;
using Together.Activity.BackgroundTasks.Tasks;

namespace Together.Activity.BackgroundTasks
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
            services.Configure<BackgroundTaskSettings>(Configuration);
            services.AddOptions();

            services.AddDbContext<TasksDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection") ??
                    throw new ArgumentNullException("Section ConnectionString");
                options.UseNpgsql(connectionString);
            });

            services.AddCap(options =>
            {

                options.UseEntityFramework<TasksDbContext>()
                    .UseRabbitMQ("rabbitmq")
                    .UseDashboard();
            });

            services.AddHostedService<ExpiredActivitiesManagerTask>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCap();
            app.UseMvc();
            //app.Map("/liveness", lapp => lapp.Run(async ctx => ctx.Response.StatusCode = 200));
        }
    }
}
