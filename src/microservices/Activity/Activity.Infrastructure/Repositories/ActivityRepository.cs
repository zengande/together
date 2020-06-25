using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Activity.Infrastructure.Data;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ActivityDbContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;
        public ActivityRepository(ActivityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Domain.AggregatesModel.ActivityAggregate.Activity Add(Domain.AggregatesModel.ActivityAggregate.Activity entity)
        {
            return _dbContext.Activities.Add(entity).Entity;
        }

        public async Task<Domain.AggregatesModel.ActivityAggregate.Activity> GetByIdAsync(int activityId)
        {
            var activity = await _dbContext
                .Activities
                .FirstOrDefaultAsync(a => a.Id == activityId);
            
            if (activity != null)
            {
                await _dbContext.Entry(activity).Collection(a => a.Attendees).LoadAsync();
            }

            return activity;
        }
    }
}
