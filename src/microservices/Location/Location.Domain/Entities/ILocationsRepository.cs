using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Together.Location.Domain.Entities
{
    public interface ILocationsRepository
    {
        Task<List<Locations>> GetByParentCodeAsync(int? parentCode);
        Task<List<Locations>> FindByLevel(int level);
    }
}
