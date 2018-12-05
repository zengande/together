using Location.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Location.API.Data
{
    public class LocationsContextSeed
    {
        private static LocationsContext _context;
        public static async Task SeedAsync(IApplicationBuilder app,
            ILoggerFactory logger)
        {
            var config = app.ApplicationServices.GetRequiredService<IOptions<LocationsSettings>>();
            _context = new LocationsContext(config);
            if (!_context.Locations.Database.GetCollection<Locations>(nameof(Locations)).AsQueryable().Any())
            {
                await SetIndexes();
                await SetProvinces();
                await SetCities();
            }
        }

        private static async Task SetProvinces()
        {
            var beijing = new Locations
            {
                Level = 1,
                LocationCode = "110000",
                CityName = "北京市"
            };
            beijing.SetLocation(116.407394, 39.904211);
            await _context.Locations.InsertOneAsync(beijing);

            await _context.Locations.InsertOneAsync(new Locations
            {
                Level = 1,
                LocationCode = "330000",
                CityName = "浙江省"
            }.SetLocation(120.152585, 30.266597));
        }
        private static async Task SetCities()
        {
            await _context.Locations.InsertManyAsync(GetZheJiangCities());
        }

        private static IEnumerable<Locations> GetZheJiangCities() => new List<Locations> {
            new Locations{Level=2, LocationCode="330500", CityName= "湖州市",ParentCode = "330000"}.SetLocation(120.086809,30.89441),
            new Locations{Level=2, LocationCode="330100", CityName= "杭州市",ParentCode = "330000"}.SetLocation(120.209789,30.24692),
            new Locations{Level=2, LocationCode="330400", CityName= "嘉兴市",ParentCode = "330000"}.SetLocation(120.75547,30.746191),
            new Locations{Level=2, LocationCode="330700", CityName= "金华市",ParentCode = "330000"}.SetLocation(119.647229,29.079208),
            new Locations{Level=2, LocationCode="331100", CityName= "丽水市",ParentCode = "330000"}.SetLocation(119.922796,28.46763),
            new Locations{Level=2, LocationCode="330200", CityName= "宁波市",ParentCode = "330000"}.SetLocation(121.622485,29.859971),
            new Locations{Level=2, LocationCode="330800", CityName= "衢州市",ParentCode = "330000"}.SetLocation(118.859457,28.970079),
            new Locations{Level=2, LocationCode="331000", CityName= "台州市",ParentCode = "330000"}.SetLocation(121.42076,28.65638),
            new Locations{Level=2, LocationCode="330600", CityName= "绍兴市",ParentCode = "330000"}.SetLocation(120.580364,30.030192),
            new Locations{Level=2, LocationCode="330300", CityName= "温州市",ParentCode = "330000"}.SetLocation(120.699361,27.993828),
            new Locations{Level=2, LocationCode="330900", CityName= "舟山市",ParentCode = "330000"}.SetLocation(122.207106,29.985553)
        };

        private static async Task SetIndexes()
        {
            var builder = Builders<Locations>.IndexKeys;
            var keys = builder.Geo2DSphere(p => p.Location);
            var indexModel = new CreateIndexModel<Locations>(keys);
            //await _context.Locations.Indexes.CreateOneAsync(keys);
            await _context.Locations.Indexes.CreateOneAsync(indexModel);
        }
    }
}
