using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Activity.Application.Dtos;

namespace Together.Activity.Application.Queries
{
    public class CatalogQueries : ICatalogQueries
    {
        private string _connectionString = string.Empty;
        public CatalogQueries(string connectionString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connectionString) ? connectionString : throw new ArgumentNullException(nameof(connectionString));
        }

        public Task<IEnumerable<CatalogDto>> GetCatalogsAsync(int? parentId)
        {
            // TODO cache
            var sql = new StringBuilder(@"SELECT
	                        appcatalogs.Id,
	                        appcatalogs.NAME
                        FROM
	                        appcatalogs ");
            if (parentId.HasValue)
            {
                sql.Append("WHERE appcatalogs.ParentId=@parentId");
            }
            else
            {
                sql.Append("WHERE ISNULL(appcatalogs.ParentId)");
            }
            sql.Append(" ORDER BY appcatalogs.Order");

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            return connection.QueryAsync<CatalogDto>(sql.ToString(), new { parentId });
        }
    }
}
