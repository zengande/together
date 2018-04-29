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

        Task<IEnumerable<ActivitySummaryViewModel>> GetActivitiesAsync();
    }
}
