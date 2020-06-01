using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Activity.Application.Dtos.Activity;

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
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var sql = "SELECT * FROM appactivities WHERE Id=@id";
            var result = await connection.QueryFirstOrDefaultAsync<ActivityDto>(sql, new { id });
            return result;
        }

    }
}
