using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Together.Activity.Infrastructure.Data;
using Nutshell.Extensions.WebHost;
using Together.Activity.API.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Together.Activity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build()
                .MigrateDbContext<ActivityDbContext>((context, services) =>
                {
                    var logger = services.GetService<ILogger<ActivityDbContextSeed>>();
                    new ActivityDbContextSeed()
                        .SeedAsync(context, logger)
                        .Wait();
                })
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:5100")
                .UseStartup<Startup>();
    }
}
