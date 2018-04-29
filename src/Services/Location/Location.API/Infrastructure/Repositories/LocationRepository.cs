using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Location.API.Data;
using Location.API.Models;
using MongoDB.Driver;

namespace Location.API.Infrastructure.Repositories
{
    public class LocationRepository
        : ILocationRepository
    {
        private readonly LocationsContext _context;
        public LocationRepository(LocationsContext context)
        {
            _context = context;
        }

        public async Task<Locations> GetAsync(int locationId, CancellationToken cancellationToken)
        {
            return (await _context.Locations.FindAsync(l => l.Id == locationId))
                .ToList(cancellationToken)
                .FirstOrDefault();
        }

        public async Task<Locations> GetAsync(string locationCode, CancellationToken cancellationToken)
        {
            var builder = Builders<Locations>.Filter;
            var filter = builder.Eq(l => l.CityCode, locationCode);
            return (await _context.Locations.FindAsync(filter, cancellationToken: cancellationToken))
                .FirstOrDefault();
        }

        public async Task<List<Locations>> GetChildLocationListAsync(int parentId, CancellationToken cancellationToken)
        {
            var builder = Builders<Locations>.Filter;
            var filter = builder.Eq(l => l.ParentId, parentId);
            return (await _context.Locations.FindAsync(filter, cancellationToken: cancellationToken))
                .ToList();
        }

        public async Task InsertLocationAsync(Locations locations, CancellationToken cancellationToken)
        {
            await _context.Locations.InsertOneAsync(locations, null, cancellationToken);
        }
    }
}
