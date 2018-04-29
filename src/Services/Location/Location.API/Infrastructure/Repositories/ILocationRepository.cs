using Location.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Location.API.Infrastructure.Repositories
{
    public interface ILocationRepository
    {
        Task<Locations> GetAsync(int locationId, CancellationToken cancellationToken);
        Task<Locations> GetAsync(string locationCode, CancellationToken cancellationToken);
        Task<List<Locations>> GetChildLocationListAsync(int parentId, CancellationToken cancellationToken);
        Task InsertLocationAsync(Locations locations, CancellationToken cancellationToken);
    }
}
