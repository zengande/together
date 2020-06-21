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
	                        AppActivities.County,
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

            return await connection.QueryFirstOrDefaultAsync<ActivityDto>(sql, new { id });
        }

        public async Task<IEnumerable<ParticipantDto>> GetActivityParticipantsAsync(int id)
        {
            var sql = @"SELECT
							AppParticipants.UserId,
							AppParticipants.Nickname,
							AppParticipants.Avatar,
							AppParticipants.Sex,
							AppParticipants.JoinTime,
							AppParticipants.IsOwner 
						FROM
							AppParticipants
						WHERE AppParticipants.ActivityId=@id
                        ORDER BY AppParticipants.JoinTime";

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            return await connection.QueryAsync<ParticipantDto>(sql, new { id });
        }
    }
}
