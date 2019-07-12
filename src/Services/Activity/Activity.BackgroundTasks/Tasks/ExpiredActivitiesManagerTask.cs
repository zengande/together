using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.BackgroundTasks.Tasks.Base;

namespace Together.Activity.BackgroundTasks.Tasks
{
    /// <summary>
    /// 将完结活动置为完结状态
    /// </summary>
    public class ExpiredActivitiesManagerTask
        : BackgroundTask
    {
        private readonly BackgroundTaskSettings _settings;
        private readonly ILogger<ExpiredActivitiesManagerTask> _logger;
        public ExpiredActivitiesManagerTask(IOptions<BackgroundTaskSettings> options,
            ILogger<ExpiredActivitiesManagerTask> logger)
        {
            _logger = logger;
            _settings = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("开始更新完结活动的任务...");
            stoppingToken.Register(() => { });
            while (stoppingToken.IsCancellationRequested == false)
            {
                var count = await ChangeExpiredActivitiesToFinished();
                if (count > 0)
                {
                    _logger.LogInformation($"已将{count}条置为完成状态");
                }
                // delay 10s
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
            await Task.CompletedTask;
        }

        private async Task<int> ChangeExpiredActivitiesToFinished()
        {
            try
            {
                using (var connection = new Npgsql.NpgsqlConnection(_settings.ConnectionString))
                {
                    connection.Open();

                    return await connection.ExecuteAsync("update \"public\".activities set \"ActivityStatusId\" =3 where (round(date_part('epoch',now() AT TIME ZONE 'Asia/Shanghai' - \"ActivityEndTime\")::NUMERIC / 60)) > 0 and \"ActivityStatusId\" in  (1,2)");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"执行完结活动时发生异常：[{e.Message}]");
            }
            return 0;
        }
    }
}
