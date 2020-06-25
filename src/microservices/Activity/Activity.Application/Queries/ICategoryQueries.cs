using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Activity.Application.Dtos;

namespace Together.Activity.Application.Queries
{
    public interface ICategoryQueries
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}
