using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Location.Application.Dto;
using Together.Location.Domain.Entities;

namespace Together.Location.Application.Services
{
    public interface ILocationsService
    {
        Task<List<LocationsDto>> GetLocationsAsync(int? parentCode);
        Task<List<LocationsDto>> GetAllCitiesAsync();
    }
}
