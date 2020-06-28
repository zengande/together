using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Together.Location.Application.Dto;
using Together.Location.Domain.Entities;

namespace Together.Location.Application.Services
{
    public class LocationsService : ILocationsService
    {
        private readonly ILocationsRepository _repository;
        public LocationsService(ILocationsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LocationsDto>> GetAllCitiesAsync()
        {
            var locations = await _repository.FindByLevel(2);
            return locations?.Select(l => new LocationsDto
            {
                Code = l.Code,
                ParentCode = l.ParentCode,
                Name = l.Name,
                MergeName = l.MergeName,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                Level = l.Level
            }).ToList();
        }

        public async Task<List<LocationsDto>> GetLocationsAsync(int? parentCode)
        {
            var locations = await _repository.GetByParentCodeAsync(parentCode);
            return locations?.Select(l => new LocationsDto
            {
                Code = l.Code,
                ParentCode = l.ParentCode,
                Name = l.Name,
                MergeName = l.MergeName,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                Level = l.Level
            }).ToList();
        }


    }
}
