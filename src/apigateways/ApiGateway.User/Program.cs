using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace ApiGateway.User
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config => config.AddJsonFile(Path.Combine("configuration", "ocelot.json"))
                    .AddEnvironmentVariables())
                .UseSerilog((context, logger) =>
                {
                    var miniLevel = context.HostingEnvironment.IsDevelopment() ? LogEventLevel.Verbose : LogEventLevel.Warning;
                    logger.WriteTo.Console(miniLevel);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
