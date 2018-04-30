using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Activity.API.Applications.Queries
{
    using Together.Activity.API.Models;

    public class ActivityQueries
        : IActivityQueries
    {
        private string _connectionString = string.Empty;
        public ActivityQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<IEnumerable<ActivitySummaryViewModel>> GetActivitiesAsync(int pageIndex, int pageSize)
        {
            var sql = @"SELECT TOP(@pageSize) * FROM (
	                        SELECT a.Id as ActivityId, a.Description as Title, a.Address,a.LimitsNum, s.Name as Status,ISNULL(c.Count,0) AS NumberOfParticipants
	                        FROM [dbo].[activities] a
	                        LEFT JOIN [dbo].[activitystatus] s ON a.ActivityStatusId=s.Id
	                        LEFT JOIN (SELECT ActivityId, count(*) AS [Count]
	                        FROM [dbo].[participant]
	                        GROUP BY [ActivityId]
	                        ) c on c.ActivityId=a.Id) r
                        WHERE (r.ActivityId NOT IN
                                  (SELECT TOP (@pageSize*(@pageIndex-1)) Id
			                         FROM [dbo].[activities]
			                         ORDER BY Id)
                        )
                        ORDER BY r.ActivityId";
            return await SqlQuery<ActivitySummaryViewModel>(sql, new { pageSize, pageIndex });
        }

        public async Task<ActivityViewModel> GetActivityAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sql = @"SELECT a.OwnerId,a.Id as ActivityId, a.Description, a.Details, a.CreateTime,a.EndTime,a.ActivityTime,a.Address,a.LimitsNum, s.Name as Status,p.Id as PId, p.Nickname, p.JoinTime,p.Avatar,p.Sex  
                            FROM[dbo].[activities] a
                            LEFT JOIN[dbo].[participant] p ON a.Id = p.ActivityId
                            LEFT JOIN[dbo].[activitystatus] s ON a.ActivityStatusId=s.Id
                            WHERE a.Id=@Id";
                var result = await connection.QueryAsync<dynamic>(sql, new { id });
                if (result.AsList().Count <= 0)
                {
                    throw new KeyNotFoundException();
                }
                return MapActivityAndParticipant(result);
            }
        }

        public async Task<IEnumerable<ActivitySummaryViewModel>> GetActivitiesByUserAsync(int userId)
        {
            var sql = @"SELECT a.Id as ActivityId, a.Description as Title, a.Address,a.LimitsNum, s.Name as Status
	                    FROM [dbo].[activities] a
	                    LEFT JOIN [dbo].[activitystatus] s ON a.ActivityStatusId=s.Id
	                    WHERE a.Id IN(
		                    SELECT DISTINCT ActivityId FROM [dbo].[participant] p
		                    WHERE p.UserId=@userId)";
            return await SqlQuery<ActivitySummaryViewModel>(sql, new { userId });
        }

        private ActivityViewModel MapActivityAndParticipant(dynamic result)
        {
            var activity = new ActivityViewModel
            {
                ActivityId = result[0].ActivityId,
                Status = result[0].Status,
                OwnerId = result[0].OwnerId,
                Description = result[0].Description,
                Details = result[0].Details,
                EndTime = result[0].EndTime,
                CreateTime = result[0].CreateTime,
                ActivityTime = result[0].ActivityTime,
                Address = result[0].Address,
                LimitsNum = result[0].LimitsNum
            };


            foreach (dynamic item in result)
            {
                if (item.PId != null)
                {
                    activity.Participants.Add(new ParticipantViewModel
                    {
                        Avatar = item.Avatar,
                        JoinTime = item.JoinTime,
                        Nickname = item.Nickname,
                        Sex = item.Sex
                    });
                }
            }

            return activity;
        }

        private async Task<IEnumerable<T>> SqlQuery<T>(string sql, object args)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<T>(sql, args);
            }
        }
    }
}
