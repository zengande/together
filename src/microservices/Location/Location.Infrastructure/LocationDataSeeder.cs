using CsvHelper;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Together.Location.Domain.Entities;

namespace Together.Location.Infrastructure
{
    public class LocationDataSeeder
    {
        private static ILogger logger;
        private static LocationDbContext ctx;
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder, ILoggerFactory loggerFactory, string rootPath)
        {
            logger = loggerFactory.CreateLogger("LocationDataSeeder");
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                ctx = scope.ServiceProvider.GetRequiredService<LocationDbContext>();

                if (!ctx.Locations.Database.GetCollection<Locations>("Locations").AsQueryable().Any())
                {
                    var cityCenters = GetCityCenters(rootPath);
                    var administrativeDistricts = await GetAdministrativeDistrictsAsync(rootPath);
                    if (administrativeDistricts != null &&
                        cityCenters != null)
                    {
                        await SetProvinces(administrativeDistricts, cityCenters);
                        await SetCities(administrativeDistricts, cityCenters);
                        await SetCounties(administrativeDistricts, cityCenters);
                    }
                }
            }
        }

        private static async Task<IEnumerable<AdministrativeDistrict>> GetAdministrativeDistrictsAsync(string rootPath)
        {
            var file = Path.Combine(rootPath, "Baidu/administrative_districts_and_towns_202003.xlsx");
            var importer = new ExcelImporter();
            var result = await importer.Import<AdministrativeDistrict>(file);
            return result.Data;
        }
        private static CityCenters GetCityCenters(string rootPath)
        {
            string json = File.ReadAllText(Path.Combine(rootPath, "Baidu/BaiduMap_cityCenter.txt"), Encoding.UTF8);
            return JsonConvert.DeserializeObject<CityCenters>(json);
        }


        private static async Task SetProvinces(IEnumerable<AdministrativeDistrict> administrativeDistricts, CityCenters cityCenters)
        {
            var provinces = administrativeDistricts.GroupBy(a => new { a.CODE_PROV, a.NAME_PROV }).Select(g => g.Key);
            if (provinces != null)
            {
                var areas = new List<Locations>();
                foreach (var province in provinces)
                {
                    Center center = null;
                    center = cityCenters.municipalities.FirstOrDefault(m => province.NAME_PROV.StartsWith(m.Name));
                    if (center == null)
                    {
                        center = cityCenters.provinces.FirstOrDefault(m => province.NAME_PROV.StartsWith(m.Name));
                    }
                    if (center == null)
                    {
                        center = cityCenters.other.FirstOrDefault(m => province.NAME_PROV.StartsWith(m.Name));
                    }

                    var location = center?.Point.Split(',', '|').Select(p => Convert.ToDouble(p)).ToArray();

                    var area = new Locations(province.CODE_PROV, province.NAME_PROV, province.NAME_PROV, 1);
                    area.SetLocation(location[0], location[1]);

                    areas.Add(area);
                }
                logger.LogInformation($"写入省级区域，一共{areas.Count}条数据");

                await ctx.Locations.InsertManyAsync(areas);
            }
        }

        private static async Task SetCities(IEnumerable<AdministrativeDistrict> administrativeDistricts, CityCenters cityCenters)
        {
            var cities = administrativeDistricts.GroupBy(a => new { a.CODE_PROV, a.NAME_PROV, a.CODE_CITY, a.NAME_CITY }).Select(g => g.Key);
            if (cities != null)
            {
                var areas = new List<Locations>();
                foreach (var city in cities)
                {
                    Center center = null;
                    center = cityCenters.provinces.FirstOrDefault(m => city.NAME_PROV.StartsWith(m.Name))?.cities.FirstOrDefault(c => city.NAME_CITY.StartsWith(c.Name));
                    if (center == null)
                    {
                        center = cityCenters.municipalities.FirstOrDefault(m => city.NAME_PROV.StartsWith(m.Name));
                    }
                    if (center == null)
                    {
                        center = cityCenters.other.FirstOrDefault(m => city.NAME_PROV.StartsWith(m.Name));
                    }
                    if (center == null)
                    {
                        center = cityCenters.provinces.FirstOrDefault(m => city.NAME_PROV.StartsWith(m.Name));
                    }

                    var location = center?.Point.Split(',', '|').Select(p => Convert.ToDouble(p)).ToArray();

                    var area = new Locations(city.CODE_CITY, city.NAME_CITY, $"{city.NAME_PROV},{city.NAME_CITY}", 2, city.CODE_PROV);
                    area.SetLocation(location[0], location[1]);

                    areas.Add(area);
                }
                logger.LogInformation($"写入市级区域，一共{areas.Count}条数据");

                await ctx.Locations.InsertManyAsync(areas);
            }
        }

        private static async Task SetCounties(IEnumerable<AdministrativeDistrict> administrativeDistricts, CityCenters cityCenters)
        {
            var counties = administrativeDistricts.GroupBy(a => new { a.NAME_PROV, a.CODE_CITY, a.NAME_CITY, a.NAME_COUN, a.CODE_COUN }).Select(g => g.Key);
            if (counties != null)
            {
                var areas = new List<Locations>();
                foreach (var county in counties)
                {
                    Center center = null;
                    center = cityCenters.provinces.FirstOrDefault(m => county.NAME_PROV.StartsWith(m.Name))?.cities.FirstOrDefault(c => county.NAME_CITY.StartsWith(c.Name));
                    if (center == null)
                    {
                        center = cityCenters.municipalities.FirstOrDefault(m => county.NAME_PROV.StartsWith(m.Name));
                    }
                    if (center == null)
                    {
                        center = cityCenters.other.FirstOrDefault(m => county.NAME_PROV.StartsWith(m.Name));
                    }
                    if (center == null)
                    {
                        center = cityCenters.provinces.FirstOrDefault(m => county.NAME_PROV.StartsWith(m.Name));
                    }

                    var location = center?.Point.Split(',', '|').Select(p => Convert.ToDouble(p)).ToArray();

                    var area = new Locations(county.CODE_COUN, county.NAME_COUN, $"{county.NAME_PROV},{county.NAME_CITY},{county.NAME_COUN}", 3, county.CODE_CITY);
                    area.SetLocation(location[0], location[1]);

                    areas.Add(area);
                }
                logger.LogInformation($"写入区级区域，一共{areas.Count}条数据");

                await ctx.Locations.InsertManyAsync(areas);
            }
        }


        class Center
        {
            [JsonProperty(PropertyName = "n")]
            public string Name { get; set; }
            [JsonProperty(PropertyName = "g")]
            public string Point { get; set; }
        }
        class Province : Center
        {
            public List<Center> cities { get; set; }
        }
        class CityCenters
        {
            public List<Center> municipalities { get; set; }
            public List<Province> provinces { get; set; }
            public List<Center> other { get; set; }
        }

        class AdministrativeDistrict
        {
            public int MAPINFO_ID { get; set; }
            public int CODE_PROV { get; set; }
            public string NAME_PROV { get; set; }
            public int CODE_CITY { get; set; }
            public string NAME_CITY { get; set; }
            public int CODE_COUN { get; set; }
            public string NAME_COUN { get; set; }
            public int CODE_TOWN { get; set; }
            public string NAME_TOWN { get; set; }
        }
    }
}
