using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Activity.Infrastructure.Data;

namespace Together.Activity.API.Infrastructure
{
    public class ActivityDbContextSeed
    {
        public async Task SeedAsync(ActivityDbContext context, ILogger<ActivityDbContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(ActivityDbContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                using (context)
                {
                    context.Database.Migrate();

                    if (!context.ActivityStatuses.Any())
                    {
                        context.ActivityStatuses.AddRange(ActivityStatus.List());
                    }

                    if (!context.AddressVisibleRules.Any())
                    {
                        context.AddressVisibleRules.AddRange(AddressVisibleRule.List());
                    }

                    await context.SaveChangesAsync();
                }
            });
        }

        private AsyncPolicy CreatePolicy(ILogger<ActivityDbContextSeed> logger, string prefix, int reties = 3)
        {
            return Policy.Handle<SqlException>()
                .WaitAndRetryAsync(retryCount: reties,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(retry * 5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogTrace($"[{prefix}] Exception : 第 {retry} 次 发生{exception.GetType().Name} - {exception.Message}的异常。");
                    });
        }
    }
}
