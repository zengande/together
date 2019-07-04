using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Together.Activity.API.Applications.Queries
{
    public class DapperQueries
    {
        protected readonly string _connectionString;
        public DapperQueries(string connectionString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connectionString) ? connectionString :
                throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IEnumerable<T>> SqlQuery<T>(string sql, object args = null)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<T>(sql, args);
            }
        }

        public async Task<T> SqlQueryFirstOrDefaultAsync<T>(string sql, object args = null)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<T>(sql, args);
            }
        }
    }
}
