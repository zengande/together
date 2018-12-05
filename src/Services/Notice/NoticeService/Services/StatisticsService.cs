using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Together.Notice.Models;
using Together.Notice.ViewModels;

namespace Together.Notice.Services
{
    public class StatisticsService
        : IStatisticsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public StatisticsService(ApplicationDbContext dbContext,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<DashboardViewModel> GetOverviewData(int days = 7)
        {
            if (days <= 0 ||
                days > 1095)
            {
                throw new ArgumentOutOfRangeException(nameof(days), $"只能在 1-1095 之间");
            }
            var nearFuture = new StringBuilder();
            var now = DateTime.Now;
            var category = new string[days];
            for (var i = days - 1; i >= 0; i--)
            {
                var date = now.AddDays(-i).ToString("yyyy-MM-dd");
                category[days - 1 - i] = date;
                nearFuture.Append($"'{date}',");
            }
            nearFuture.Remove(nearFuture.Length - 1, 1);
            var sql = $"SELECT \"count\"(*) as \"Count\", \"public\".\"NoticeRecords\".\"Status\", to_char(\"public\".\"NoticeRecords\".\"SendingTime\", 'yyyy-mm-dd') as \"Date\" FROM \"public\".\"NoticeRecords\" GROUP BY to_char(\"public\".\"NoticeRecords\".\"SendingTime\", 'yyyy-mm-dd'), \"public\".\"NoticeRecords\".\"Status\" HAVING to_char(\"public\".\"NoticeRecords\".\"SendingTime\",'yyyy-mm-dd') in({nearFuture.ToString()})";
            var data = await SqlQuery<NearFutureStatistics>(sql);
            if (data != null)
            {
                var result = new DashboardViewModel
                {
                    Total = data.Sum(r => r.Count),
                    Success = data.Where(r => r.Status == (int)NotificationStatus.Success).Sum(r => r.Count),
                    Failure = data.Where(r => r.Status == (int)NotificationStatus.Failure).Sum(r => r.Count),
                    Category = category.ToList(),
                    Series = GetSeries(data, days)
                };
                return result;
            }
            return null;
        }

        private List<object> GetSeries(IEnumerable<NearFutureStatistics> results, int days = 7)
        {
            var result = new List<object>();
            var success = GetSerie(results, NotificationStatus.Success);
            var failure = GetSerie(results, NotificationStatus.Failure);
            var all = GetSerie(results, NotificationStatus.Unknown);

            result.Add(new
            {
                data = GetNearFutureData(all, days)
            });
            result.Add(new
            {
                data = GetNearFutureData(success, days)
            });
            result.Add(new
            {
                data = GetNearFutureData(failure, days)
            });


            return result;
        }

        private int[] GetNearFutureData(Dictionary<string, int> dic, int days = 7)
        {
            var result = new int[days];
            var now = DateTime.Now;
            for (var i = days - 1; i >= 0; i--)
            {
                var date = now.AddDays(-i).ToString("yyyy-MM-dd");
                if (dic.ContainsKey(date))
                {
                    result[days - 1 - i] = dic[date];
                    continue;
                }
                result[days - 1 - i] = 0;
            }
            return result;
        }

        private Dictionary<string, int> GetSerie(IEnumerable<NearFutureStatistics> results, NotificationStatus status)
        {
            List<NearFutureStatistics> series = null;
            if (status == NotificationStatus.Unknown)
            {
                series = results?.ToList();
            }
            else
            {
                series = results.Where(r => r.Status == (int)status)?.ToList();
            }
            var dic = new Dictionary<string, int>();
            foreach (var item in series)
            {
                var key = item.Date;
                if (dic.ContainsKey(key))
                {
                    dic[key] += item.Count;
                    continue;
                }
                dic.Add(key, item.Count);
            }
            return dic;
        }

        private async Task<IEnumerable<T>> SqlQuery<T>(string sql, object param = null)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<T>(sql, param);
            }
        }
    }

    /// <summary>
    /// 近期统计数据
    /// </summary>
    public class NearFutureStatistics
    {
        public string Date { get; set; }
        public int Count { get; set; }
        public int Status { get; set; }
    }

}
