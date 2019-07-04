using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System;
using Together.Extensions.WebHost;
using Together.Identity.API.Data;

namespace Together.Identity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build()
                .MigrateDbContext<IdentityDbContext>((context, service) =>
                {
                    // seed roles
                    if (!context.Roles.Any())
                    {
                        context.Roles.Add(new Microsoft.AspNetCore.Identity.IdentityRole
                        {
                            Id = "6170c51e-1e4f-4f1f-bbcf-f4bd06937716",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR",
                            ConcurrencyStamp = Guid.NewGuid().ToString("D")
                        });
                        context.SaveChanges();
                    }
                })
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration((hostingContext, builder) =>
                //{
                //    builder
                //    .AddApollo(builder.Build().GetSection("apollo"))
                //    .AddDefault()
                //    .AddNamespace("TEST1.TogetherShared")
                //    .AddNamespace("ClientUrls");
                //})
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    config.AddEnvironmentVariables();
                })
                .UseStartup<Startup>();
    }
}
