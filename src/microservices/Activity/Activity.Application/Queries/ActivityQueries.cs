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

        public async Task<ActivityDto> GetActivityByIdAsync(int id, bool showAddress = false)
        {
            var sql = new StringBuilder(@"SELECT AppActivities.Title, AppActivities.EndRegisterTime, AppActivities.ActivityStartTime, ");
            if (showAddress)
            {
                sql.Append("AppActivities.DetailAddress, AppActivities.City, AppActivities.County, AppActivities.Longitude, AppActivities.Latitude, ");
            }
            sql.Append(@"AppActivities.LimitsNum, AppActivities.ActivityEndTime, AppActivities.Content, AppActivities.Id, AppActivities.ActivityStatusId FROM AppActivities WHERE AppActivities.Id = @id");

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            return await connection.QueryFirstOrDefaultAsync<ActivityDto>(sql.ToString(), new { id });
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

        public async Task<bool> IsJoinedAsync(int activityId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            var sql = @"SELECT
	                        COUNT(*)
                        FROM
                            appparticipants
                        WHERE ActivityId=@activityId AND UserId=@userId
                        ";
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            return await connection.QueryFirstOrDefaultAsync<int>(sql, new { activityId, userId }) > 0;
        }
    }
}
