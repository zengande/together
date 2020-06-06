using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Together.Activity.Application.Dtos;
using Together.Activity.Domain.AggregatesModel.CatalogAggregate;

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
	                        AppCatalogs.Id,
	                        AppCatalogs.NAME
                        FROM
	                        AppCatalogs ");
            if (parentId.HasValue)
            {
                sql.Append("WHERE AppCatalogs.ParentId=@parentId");
            }
            else
            {
                sql.Append("WHERE ISNULL(AppCatalogs.ParentId)");
            }
            sql.Append(" ORDER BY AppCatalogs.Order");

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            return connection.QueryAsync<CatalogDto>(sql.ToString(), new { parentId });
        }
    }
}
