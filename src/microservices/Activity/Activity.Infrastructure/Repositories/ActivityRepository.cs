using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
