using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Activity.Application.Dtos;

namespace Together.Activity.Application.Queries
{
    public interface ICatalogQueries
    {
        Task<IEnumerable<CatalogDto>> GetCatalogsAsync(int? parentId);
    }
}
