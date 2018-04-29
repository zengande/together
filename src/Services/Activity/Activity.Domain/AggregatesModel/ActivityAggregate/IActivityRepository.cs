using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Activity.Domain.SeedWork;

namespace Together.Activity.Domain.AggregatesModel.ActivityAggregate
{
    public interface IActivityRepository
        : IRepository<Activity>
    {
        Task<Activity> AddAsync(Activity activity);

        Task UpdateAsync(Activity activity);

        Task<Activity> GetAsync(int activityId);
    }
}
