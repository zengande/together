﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Data.SqlClient;

namespace Nutshell.Extensions.WebHost
{
    public static class MigrateDbContextExtensions
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation($"执行数据库初始化操作：context {typeof(TContext).Name}");
                    var retry = Policy.Handle<Exception>()
                            .WaitAndRetry(new TimeSpan[]
                            {
                             TimeSpan.FromSeconds(5),
                             TimeSpan.FromSeconds(10),
                             TimeSpan.FromSeconds(15),
                            });
                    retry.Execute(() =>
                    {
                        context.Database.Migrate();
                        seeder(context, services);
                        context.Dispose();
                    });

                    logger.LogInformation("数据库初始化成功！");
                }
                catch (Exception e)
                {
                    logger.LogError($"数据库初始化失败：{e.Message}");
                }
            }
            return webHost;
        }
    }
}
