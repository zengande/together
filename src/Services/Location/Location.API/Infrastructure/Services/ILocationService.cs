using Location.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Location.API.Infrastructure.Services
{
    public interface ILocationService
    {
        Task<List<Locations>> GetCityListByParentId(string parentCode);

        Task<List<Locations>> Search(string keyword);

        Task<Locations> GetAsync(string locationCode);
        Task<List<Locations>> GetLocationsAsync(int level);
    }
}
