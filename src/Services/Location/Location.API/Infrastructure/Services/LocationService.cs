using Location.API.Infrastructure.Repositories;
using Location.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Location.API.Infrastructure.Services
{
    public class LocationService
        : ILocationService
    {
        private readonly LocationRepository _locationRepository;
        public LocationService(LocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public List<Locations> GetCityListByParentId(int parentId)
        {
            //_locationRepository.GetChildLocationListAsync(parentId)
            return null;
        }
    }
}
