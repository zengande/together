using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Domain.AggregatesModel.ActivityAggregate
{
    public interface IActivityRepository : IRepository<Activity>
    {
        Task<Activity> GetByIdAsync(int activityId);
    }
}
