using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.Linq;
using Together.Activity.Domain.AggregatesModel.CatalogAggregate;
using Together.Activity.Infrastructure.Data;
using Together.BuildingBlocks.Infrastructure.Data;

namespace Activity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build()
                .MigrateDatabase<ActivityDbContext>((context, _) =>
                {
                    if (!context.Categories.Any())
                    {
                        var categories = new Category[] {
                            new Category("户外与冒险", "",1),
                            new Category("技术", "",2),
                            new Category("健康与养生", "",3),
                            new Category("运动与健身", "",4),
                            new Category("写作", "",5),
                            new Category("音乐", "",6),
                            new Category("电影", "",7),
                            new Category("艺术", "",8),
                            new Category("手工艺", "",9),
                            new Category("宠物", "",10),
                            new Category("社交", "",11),
                            new Category("时尚与美容", "",12),
                            new Category("语言与文化", "",13),
                            new Category("ְ职业与商业", "",14),
                        };
                        context.Categories.AddRange(categories);
                    }

                    context.SaveChanges();
                })
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    config.AddEnvironmentVariables();
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
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
