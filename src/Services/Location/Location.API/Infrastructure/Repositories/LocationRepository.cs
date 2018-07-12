using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Location.API.Data;
using Location.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Location.API.Infrastructure.Repositories
{
    public class LocationRepository
        : ILocationRepository
    {
        private readonly LocationsContext _context;
        public LocationRepository(IOptions<LocationsSettings> settings)
        {
            _context = new LocationsContext(settings);
        }

        public async Task<Locations> GetAsync(string locationCode, CancellationToken cancellationToken)
        {
            return (await _context.Locations.FindAsync(l => l.LocationCode == locationCode))
                .FirstOrDefault();
        }

        //public async Task<Locations> GetAsync(string locationCode, CancellationToken cancellationToken)
        //{
        //    var builder = Builders<Locations>.Filter;
        //    var filter = builder.Eq(l => l.CityCode, locationCode);
        //    return (await _context.Locations.FindAsync(filter, cancellationToken: cancellationToken))
        //        .FirstOrDefault();
        //}

        public async Task<List<Locations>> GetChildLocationListAsync(string parentCode, CancellationToken cancellationToken)
        {
            var filter = Builders<Locations>.Filter.Eq(l => l.ParentCode, parentCode);
            var sort = Builders<Locations>.Sort.Ascending(l => l.LocationCode);
            var result = _context.Locations.Find(filter)
                .Sort(sort);
            return await result?.ToListAsync();
        }

        public async Task InsertLocationAsync(Locations locations, CancellationToken cancellationToken)
        {
            await _context.Locations.InsertOneAsync(locations, null, cancellationToken);
        }

        public async Task<List<Locations>> Search(string keyword)
        {
            var filter = Builders<Locations>.Filter.Where(l => l.CityName.Contains(keyword));
            var locations = (await _context.Locations.FindAsync(filter))?.ToList();
            return locations;
        }
    }
}
