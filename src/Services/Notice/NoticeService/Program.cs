using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Together.Extensions.WebHost;

namespace Together.Notice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build()
                .MigrateDbContext<ApplicationDbContext>(async (context, provider) =>
                {
                    if (!context.EmailTemplates.Any())
                    {
                        await context.EmailTemplates.AddAsync(new Models.EmailTemplate
                        {
                            Id = 1,
                            Template = "点击<a href='[$link]'>这里</a>激活您的邮箱，如果不能跳转",
                            Title = "激活您的邮箱"
                        });
                        await context.SaveChangesAsync();
                    }
                }).Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddEnvironmentVariables();
                });
    }
}
