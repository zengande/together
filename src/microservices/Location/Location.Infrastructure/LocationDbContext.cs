using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Together.Location.Domain.Entities;

namespace Together.Location.Infrastructure
{
    public class LocationDbContext
    {
        private readonly IMongoDatabase _database = null;

        public LocationDbContext(IOptions<LocationDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Locations> Locations 
            => _database.GetCollection<Locations>("Locations");
    }
}
