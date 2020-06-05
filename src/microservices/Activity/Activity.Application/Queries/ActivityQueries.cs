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
	                        appactivities.Title,
	                        appactivities.EndRegisterTime,
	                        appactivities.ActivityStartTime,
	                        appactivities.DetailAddress,
	                        appactivities.City,
	                        appactivities.Province,
	                        appactivities.Longitude,
	                        appactivities.Latitude,
	                        appactivities.LimitsNum,
	                        appactivities.ActivityEndTime,
	                        appactivities.Content,
	                        appactivities.AddressVisibleRuleId,
	                        appactivities.Id,
	                        appactivities.ActivityStatusId 
                        FROM
	                        appactivities
                        WHERE appactivities.Id=@id";

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var result = await connection.QueryFirstOrDefaultAsync<ActivityDto>(sql, new { id });
            return result;
        }

        public Task<IEnumerable<ParticipantDto>> GetActivityParticipantsAsync(int id)
        {
            var sql = @"SELECT
							appparticipants.UserId,
							appparticipants.Nickname,
							appparticipants.Avatar,
							appparticipants.Sex,
							appparticipants.JoinTime,
							appparticipants.IsOwner 
						FROM
							appparticipants
						WHERE appparticipants.ActivityId=@id
                        ORDER BY appparticipants.JoinTime";

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            return connection.QueryAsync<ParticipantDto>(sql, new { id });
        }
    }
}
