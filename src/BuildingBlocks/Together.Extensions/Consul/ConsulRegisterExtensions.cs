using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using System;

namespace Together.Extensions.Consul
{
    public static class ConsulRegisterExtensions
    {
        /// <summary>
        /// 添加consul
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsulClient(this IServiceCollection services)
        {
            services.AddSingleton<IConsulClient>(p =>
            {
                var options = p.GetRequiredService<IOptions<ServiceRegisterOptions>>()?.Value ??
                    throw new ArgumentNullException(nameof(ServiceRegisterOptions));
                var configOverride = new Action<ConsulClientConfiguration>(cfg =>
                {
                    if (!string.IsNullOrEmpty(options.ConsulEndpoint))
                    {
                        cfg.Address = new Uri(options.ConsulEndpoint);
                    }
                });


                return new ConsulClient(configOverride);
            });
            return services;
        }


        /// <summary>
        /// 注册consul服务
        /// </summary>
        /// <param name="app"></param>
        /// <param name="lifetime"></param>
        /// <param name="consul"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder RegisterConsulService(this IApplicationBuilder app,
            IApplicationLifetime lifetime,
            IConsulClient consul,
            ILoggerFactory loggerFactory)
        {
            try
            {
                var options = app.ApplicationServices.GetRequiredService<IOptions<ServiceRegisterOptions>>()?.Value ??
                    throw new ArgumentNullException(nameof(ServiceRegisterOptions));
                var logger = loggerFactory.CreateLogger(typeof(ConsulRegisterExtensions));
                var retry = Policy.Handle<Exception>()
                    .WaitAndRetry(new TimeSpan[] {
                        TimeSpan.FromSeconds(3),
                        TimeSpan.FromSeconds(10),
                        TimeSpan.FromSeconds(30),
                    }, (exception, timespan, count, context) =>
                    {
                        logger.LogError($"注册服务失败：service name [{options.ServiceName}]，错误信息：{exception.Message}");
                        logger.LogError($"{timespan.TotalSeconds}秒后执行第{count}此重试！");
                    });

                retry.Execute(() =>
                {
                    var uri = new Uri(options.ServiceRegisterUrl);
                    var healthCheck = options.HealthCheckUrl ?? $"{options.ServiceRegisterUrl}/healthcheck";
                    var serviceId = $"{options.ServiceName}_{uri.Host}:{uri.Port}";
                    var httpCheck = new AgentServiceCheck
                    {
                        DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                        Interval = TimeSpan.FromSeconds(30),
                        HTTP = healthCheck
                    };
                    var registration = new AgentServiceRegistration
                    {
                        Checks = new[] { httpCheck },
                        Address = uri.Host,
                        ID = serviceId,
                        Name = options.ServiceName,
                        Port = uri.Port
                    };

                    lifetime.ApplicationStarted.Register(() =>
                    {
                        consul.Agent.ServiceRegister(registration).GetAwaiter().GetResult();
                        logger.LogInformation($"服务【{options.ServiceName}】注册成功！");
                    });

                    lifetime.ApplicationStopping.Register(() =>
                    {
                        consul.Agent.ServiceDeregister(serviceId).GetAwaiter().GetResult();
                        logger.LogInformation($"服务【{options.ServiceName}】已取消注册！");
                    });
                });
            }
            catch (Exception)
            {

            }

            return app;
        }
    }
}
