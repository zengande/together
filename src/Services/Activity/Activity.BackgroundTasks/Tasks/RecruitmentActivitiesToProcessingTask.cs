using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.BackgroundTasks.Tasks.Base;

namespace Together.Activity.BackgroundTasks.Tasks
{
    public class RecruitmentActivitiesToProcessingTask
        : BackgroundTask
    {
        private readonly BackgroundTaskSettings _settings;
        private readonly ILogger<RecruitmentActivitiesToProcessingTask> _logger;
        public RecruitmentActivitiesToProcessingTask(IOptions<BackgroundTaskSettings> options,
            ILogger<RecruitmentActivitiesToProcessingTask> logger)
        {
            _logger = logger;
            _settings = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                var changed = await ChangeRecruitmentCompletedActivitiesToProcessing();

                _logger.LogInformation($"已将{changed}条置为进行状态");

                // delay 10s
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task<int> ChangeRecruitmentCompletedActivitiesToProcessing()
        {
            try
            {
                using (var connection = new Npgsql.NpgsqlConnection(_settings.ConnectionString))
                {
                    return await connection.ExecuteAsync("UPDATE activities SET \"ActivityStatusId\" =2 WHERE (round(date_part('epoch',now() AT TIME ZONE 'Asia/Shanghai' - \"EndRegisterTime\")::NUMERIC / 60)) > 0 and \"ActivityStatusId\" in (1)");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return 0;
            }
        }
    }
}
