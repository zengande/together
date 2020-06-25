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
                            new Category("������ð��", "",1),
                            new Category("����", "",2),
                            new Category("����������", "",3),
                            new Category("�˶��뽡��", "",4),
                            new Category("д��", "",5),
                            new Category("����", "",6),
                            new Category("��Ӱ", "",7),
                            new Category("����", "",8),
                            new Category("�ֹ���", "",9),
                            new Category("����", "",10),
                            new Category("�罻", "",11),
                            new Category("ʱ��������", "",12),
                            new Category("�������Ļ�", "",13),
                            new Category("ְҵ����ҵ", "",14),
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
