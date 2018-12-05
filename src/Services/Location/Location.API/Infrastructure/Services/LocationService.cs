using Location.API.Infrastructure.Repositories;
using Location.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Location.API.Infrastructure.Services
{
    public class LocationService
        : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Locations> GetAsync(string locationCode)
        {
            return await _locationRepository.GetAsync(locationCode, default(CancellationToken));
        }

        public async Task<List<Locations>> GetCityListByParentId(string parentCode)
        {
            return await _locationRepository.GetChildLocationListAsync(parentCode, default(CancellationToken));
        }

        public async Task<List<Locations>> GetLocationsAsync(int level)
        {
            return await _locationRepository.GetLocationsAsync(level);
        }

        public async Task<List<Locations>> Search(string keyword)
        {
            return await _locationRepository.Search(keyword);
        }
    }
}
