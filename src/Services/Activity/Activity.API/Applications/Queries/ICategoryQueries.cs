using System.Collections.Generic;
using System.Threading.Tasks;
using Together.Activity.API.Models;

namespace Together.Activity.API.Applications.Queries
{
    public interface ICategoryQueries
    {
        Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync(int? parentId);
    }
}
