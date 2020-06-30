using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Location.Domain.Entities;

namespace Together.Location.Infrastructure.Repositories
{
    public class LocationsRepository : ILocationsRepository
    {
        private readonly LocationDbContext _dbContext;
        public LocationsRepository(LocationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Locations>> FindByLevel(int level)
        {
            var filter = Builders<Locations>.Filter.Eq(l => l.Level, level);
            return _dbContext.Locations.Find(filter).ToListAsync();
        }

        public Task<List<Locations>> GetByParentCodeAsync(int? parentCode)
        {
            var filter = Builders<Locations>.Filter.Eq(l => l.ParentCode, parentCode);
            return _dbContext.Locations.Find(filter).ToListAsync();
        }
    }
}
