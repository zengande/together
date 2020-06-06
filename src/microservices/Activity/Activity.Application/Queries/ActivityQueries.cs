using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Activity.Application.Dtos;

namespace Together.Activity.Application.Queries
{
    public class ActivityQueries : IActivityQueries
    {
        private string _connectionString = string.Empty;
        public ActivityQueries(string connectionString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connectionString) ? connectionString : throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<ActivityDto> GetActivityByIdAsync(int id)
        {
            var sql = @"SELECT
	                        AppActivities.Title,
	                        AppActivities.EndRegisterTime,
	                        AppActivities.ActivityStartTime,
	                        AppActivities.DetailAddress,
	                        AppActivities.City,
	                        AppActivities.Province,
	                        AppActivities.Longitude,
	                        AppActivities.Latitude,
	                        AppActivities.LimitsNum,
	                        AppActivities.ActivityEndTime,
	                        AppActivities.Content,
	                        AppActivities.AddressVisibleRuleId,
	                        AppActivities.Id,
	                        AppActivities.ActivityStatusId 
                        FROM
	                        AppActivities
                        WHERE AppActivities.Id=@id";

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var result = await connection.QueryFirstOrDefaultAsync<ActivityDto>(sql, new { id });
            return result;
        }

        public Task<IEnumerable<ParticipantDto>> GetActivityParticipantsAsync(int id)
        {
            var sql = @"SELECT
							AppActivities.UserId,
							AppActivities.Nickname,
							AppActivities.Avatar,
							AppActivities.Sex,
							AppActivities.JoinTime,
							AppActivities.IsOwner 
						FROM
							AppActivities
						WHERE AppActivities.ActivityId=@id
                        ORDER BY AppActivities.JoinTime";

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            return connection.QueryAsync<ParticipantDto>(sql, new { id });
        }
    }
}
