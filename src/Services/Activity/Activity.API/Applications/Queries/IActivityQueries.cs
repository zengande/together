using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Activity.API.Applications.Dtos;
using Together.Activity.API.Models;

namespace Together.Activity.API.Applications.Queries
{
    public interface IActivityQueries
    {
        Task<ActivityDetailDto> GetActivityAsync(int id);

        Task<IEnumerable<ActivitySummaryDto>> GetActivitiesAsync(int pageIndex, int pageSize, int status = 2);

        Task<IEnumerable<ActivitySummaryDto>> GetLatestActivitiesNearby(int pageIndex, int pageSize, string location);

        Task<IEnumerable<ActivitySummaryDto>> GetActivitiesByUserAsync(string userId);

        Task<bool> AlreadyJoined(int activityId, string userId);
    }
}
