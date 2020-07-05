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
    public class CategoryQueries : ICategoryQueries
    {
        private string _connectionString = string.Empty;
        public CategoryQueries(string connectionString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connectionString) ? connectionString : throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            // TODO 添加缓存
            var sql = @"SELECT
	                        AppCategories.Id,
	                        AppCategories.NAME
                        FROM
	                        AppCategories
                        ORDER BY AppCategories.Order";
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            return await connection.QueryAsync<CategoryDto>(sql);
        }
    }
}
