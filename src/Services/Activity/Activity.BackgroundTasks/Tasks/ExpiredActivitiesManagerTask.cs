using Together.Activity.BackgroundTasks.Tasks.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Together.Activity.BackgroundTasks.IntegrationEvents.Events;
using DotNetCore.CAP;

namespace Together.Activity.BackgroundTasks.Tasks
{
    public class ExpiredActivitiesManagerTask
        : BackgroundTask
    {
        private readonly IServiceProvider _provider;
        private readonly BackgroundTaskSettings _settings;
        public ExpiredActivitiesManagerTask(IOptions<BackgroundTaskSettings> options,
            IServiceProvider provider)
        {
            _provider = provider;
            _settings = options.Value;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            stoppingToken.Register(() => { });

            while (stoppingToken.IsCancellationRequested == false)
            {
                await CheckExpiredActivities();

                // delay 10s
                await Task.Delay(100000, stoppingToken);

            }
            await Task.CompletedTask;
        }

        private async Task CheckExpiredActivities()
        {
            try
            {
                using (IServiceScope scope = _provider.CreateScope())
                {
                    var publisher = scope.ServiceProvider.GetRequiredService<ICapPublisher>();
                    var activities = GetExpiredActivities();
                    foreach (var activity in activities)
                    {
                        await publisher.PublishAsync("Together.Activity.BackgroundTasks.ActivityExpired", new ActivityExpiredIntegrationEvent { ActivityId = activity });
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private IEnumerable<int> GetExpiredActivities()
        {
            IEnumerable<int> result = new List<int>();
            using (var connection = new SqlConnection(_settings.ConnectionString))
            {
                try
                {
                    connection.Open();

                    result = connection.Query<int>(@"select * from [dbo].[activities] 
                        where ActivityStatusId = 2 and DATEDIFF(minute, [EndTime], GETDATE()) > 1");
                }
                catch
                {

                }
            }
            return result;
        }
    }
}
