using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Activity.API.Models;

namespace Together.Activity.API.Applications.Queries
{
    public class CategoryQueries
        : DapperQueries, ICategoryQueries
    {
        public CategoryQueries(string connectionString) : base(connectionString) { }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync(int? parentId)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT \"Id\",\"Text\",\"Key\",\"ParentId\",\"Sort\" FROM categories ");
            if (parentId.HasValue)
            {
                sql.Append($"WHERE \"ParentId\" = {parentId.Value} ");
            }
            else
            {
                sql.Append("WHERE \"ParentId\" is null ");
            }
            sql.Append("ORDER BY \"Sort\"");
            var categories = await SqlQuery<CategoryViewModel>(sql.ToString());
            return categories;
        }
    }
}
