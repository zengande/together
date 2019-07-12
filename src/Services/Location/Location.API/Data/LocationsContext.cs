using Location.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Linq;

namespace Location.API.Data
{
    public class LocationsContext
    {
        private IMongoDatabase _database;
        private LocationsSettings _settings;

        public LocationsContext(IOptions<LocationsSettings> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.MongoConnentionString);
            if (client != null)
            {
                _database = client.GetDatabase(_settings.LocationsDatabaseName);
            }
        }

        public IMongoCollection<Locations> Locations
        {
            get
            {
                var list = _database.ListCollections()
                    .ToList().
                    Select(a => a["name"].AsString);
                if (!list.Any(a => a.Equals(nameof(Locations), StringComparison.CurrentCultureIgnoreCase)))
                {
                    _database.CreateCollection(nameof(Locations));
                }
                return _database.GetCollection<Locations>(nameof(Locations));
            }
        }

        public IMongoCollection<UserLocation> UserLocation
        {
            get
            {
                var list = _database.ListCollections()
                    .ToList().
                    Select(a => a["name"].AsString);
                if (!list.Any(a => a.Equals(nameof(UserLocation), StringComparison.CurrentCultureIgnoreCase)))
                {
                    _database.CreateCollection(nameof(UserLocation));
                }
                return _database.GetCollection<UserLocation>(nameof(UserLocation));
            }
        }
    }
}
