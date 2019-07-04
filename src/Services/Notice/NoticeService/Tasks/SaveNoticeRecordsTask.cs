using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nutshell.Common.Cache;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Together.Notice.Models;
using Together.Notice.Services;

namespace Together.Notice.Tasks
{
    /// <summary>
    /// 将redis中的缓存数据定时保存到数据库
    /// </summary>
    public class SaveNoticeRecordsTask
        : BackgroundService
    {
        private readonly IRedisCacheService _redis;
        private readonly INoticeRecordService _recordService;
        private readonly ILogger<SaveNoticeRecordsTask> _logger;
        private Timer _timer;
        public SaveNoticeRecordsTask(IRedisCacheService redis,
            IServiceProvider provider,
            ILogger<SaveNoticeRecordsTask> logger)
        {
            _redis = redis;
            _logger = logger;
            using (var scope = provider.CreateScope())
            {
                _recordService = scope.ServiceProvider.GetRequiredService<INoticeRecordService>();
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(async (state) =>
            {
                _logger.LogInformation("Get records from Redis.");

                var json = await _redis.ListLeftPopAsync(Constants.NoticeRecordRedisKey);
                if (string.IsNullOrEmpty(json) == false)
                {
                    var record = JsonConvert.DeserializeObject<List<NoticeRecord>>(json);
                    await _recordService.RecordAsync(record);
                }
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            base.Dispose();
            _timer?.Dispose();
        }
    }
}
