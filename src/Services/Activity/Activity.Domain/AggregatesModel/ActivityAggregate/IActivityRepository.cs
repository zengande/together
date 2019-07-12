using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        Task<Activity> FindOneAsync(Expression<Func<Activity, bool>> where);
        Task<IQueryable<Activity>> FindListAsync(List<int> activityIds);
    }
}
