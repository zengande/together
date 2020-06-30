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

        public async Task<ActivityDto> GetActivityByIdAsync(int id, string userId = null)
        {
            var isCollected = await new CollectionQueries(_connectionString).IsCollected(id, userId);
            var isJoined = await IsJoinedAsync(id, userId);

            var sql = new StringBuilder(@"SELECT AppActivities.Title, AppActivities.EndRegisterTime, AppActivities.ActivityStartTime, ");
            if (isJoined)
            {
                sql.Append("AppActivities.DetailAddress, AppActivities.City, AppActivities.County, AppActivities.Longitude, AppActivities.Latitude, ");
            }

            if (string.IsNullOrEmpty(userId))
            {
                sql.Append("FALSE AS IsCreator, ");
            }
            else
            {
                sql.Append(@"CASE AppActivities.CreatorId WHEN @userId THEN TRUE ELSE FALSE END AS IsCreator, ");
            }

            sql.Append(@"(@isJoined OR AppActivities.AddressVisibleRuleId=1) AS ShowAddress, @isJoined AS IsJoined, @isCollected AS IsCollected, AppActivities.LimitsNum, AppActivities.ActivityEndTime, AppActivities.Content, AppActivities.Id, AppActivities.ActivityStatusId, (SELECT COUNT(*) FROM AppAttendees WHERE ActivityId=@id) AS NumOfP FROM AppActivities WHERE AppActivities.Id = @id");

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            return await connection.QueryFirstOrDefaultAsync<ActivityDto>(sql.ToString(), new { id, isJoined, userId, isCollected });
        }

        public async Task<IEnumerable<AttendeeDto>> GetActivityAttendeesAsync(int id)
        {
            var sql = @"SELECT
							AppAttendees.UserId,
							AppAttendees.Nickname,
							AppAttendees.Avatar,
							AppAttendees.Sex,
							AppAttendees.JoinTime,
							AppAttendees.IsOwner 
						FROM
							AppAttendees
						WHERE AppAttendees.ActivityId=@id
                        ORDER BY AppAttendees.JoinTime";

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            return await connection.QueryAsync<AttendeeDto>(sql, new { id });
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
                            AppAttendees
                        WHERE ActivityId=@activityId AND UserId=@userId
                        ";
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            return await connection.QueryFirstOrDefaultAsync<int>(sql, new { activityId, userId }) > 0;
        }
    }
}
