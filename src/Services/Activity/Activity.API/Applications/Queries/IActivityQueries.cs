using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Activity.API.Models;

namespace Together.Activity.API.Applications.Queries
{
    public interface IActivityQueries
    {
        Task<ActivityViewModel> GetActivityAsync(int id);

        Task<IEnumerable<ActivitySummaryViewModel>> GetActivitiesAsync(int pageIndex, int pageSize, int status = 2);

        Task<IEnumerable<ActivitySummaryViewModel>> GetActivitiesByUserAsync(string userId);
    }
}
