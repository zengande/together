using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Together.Activity.API.Tasks
{
    public class AutoChangeActivityStatusTask : BackgroundService
    {
        private readonly ILogger<AutoChangeActivityStatusTask> _logger;
        private readonly IConfiguration _configuration;
        public AutoChangeActivityStatusTask(ILogger<AutoChangeActivityStatusTask> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var connectionString = _configuration.GetConnectionString("Default");
                using var connection = new MySqlConnection(connectionString);

                var now = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                var sql = @"UPDATE appactivities SET ActivityStatusId = 2 WHERE DATE_FORMAT( EndRegisterTime, '%Y%m%d%H%i%S' ) < @now AND DATE_FORMAT( ActivityStartTime, '%Y%m%d%H%i%S' ) < @now AND DATE_FORMAT( ActivityEndTime, '%Y%m%d%H%i%S' ) > @now AND ActivityStatusId=1;UPDATE appactivities SET ActivityStatusId = 3 WHERE DATE_FORMAT( EndRegisterTime, '%Y%m%d%H%i%S' ) < @now AND DATE_FORMAT( ActivityStartTime, '%Y%m%d%H%i%S' ) < @now AND DATE_FORMAT( ActivityEndTime, '%Y%m%d%H%i%S' ) < @now AND ActivityStatusId = 2";

                var result = await connection.ExecuteAsync(sql, new { now });
                _logger.LogInformation($"自动改变{result}个活动的状态");
                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }
    }
}
