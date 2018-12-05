using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Together.Extensions.WebHost;
using Together.UserGroup.API.Infrastructure.Data;

namespace Together.UserGroup.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build()
                .MigrateDbContext<UserGroupDbContext>((_, __) => { }).Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(options =>
                {
                    options.AddEnvironmentVariables();
                })
                .UseStartup<Startup>();
    }
}
