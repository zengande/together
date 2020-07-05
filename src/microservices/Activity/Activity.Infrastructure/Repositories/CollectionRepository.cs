using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Activity.Domain.AggregatesModel.CollectionAggregate;
using Together.Activity.Infrastructure.EntityFrameworkCore;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Infrastructure.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly ActivityDbContext _dbContext;
        public CollectionRepository(ActivityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public Collection Add(Collection entity)
        {
            return _dbContext.Add(entity).Entity;
        }

        public Task<Collection> GetAsync(int activityId, string userId)
        {
            return _dbContext.Collections.FirstOrDefaultAsync(c => c.ActivityId == activityId && c.UserId == userId);
        }
    }
}
