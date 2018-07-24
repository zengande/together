using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;

namespace Api.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args);
            builder.ConfigureAppConfiguration((hostingContext, config) =>
             {
                 config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                     .AddJsonFile("appsettings.json", true, true)
                     .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                     .AddOcelot()
                     .AddEnvironmentVariables();
                 //var files = Directory.GetFiles($"{hostingContext.HostingEnvironment.ContentRootPath}/OcelotConfigurations");
                 //if (files != null)
                 //{
                 //    foreach (var file in files)
                 //    {
                 //        config.AddJsonFile(file);
                 //    }
                 //}

             })
               .UseUrls("http://localhost:8000")
               .UseStartup<Startup>();
            return builder;
        }

    }
}
