using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Together.Activity.Application.Queries
{
    public class CollectionQueries : ICollectionQueries
    {
        private string _connectionString = string.Empty;
        public CollectionQueries(string connectionString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connectionString) ? connectionString : throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<bool> IsCollected(int activityId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            var sql = @"SELECT
	                        COUNT(*)
                        FROM
                            AppCollections
                        WHERE ActivityId=@activityId AND UserId=@userId";
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            return await connection.QueryFirstOrDefaultAsync<int>(sql, new { activityId, userId }) > 0;
        }
    }
}
