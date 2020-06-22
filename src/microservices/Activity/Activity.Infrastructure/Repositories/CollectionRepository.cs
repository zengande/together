using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Domain.AggregatesModel.CollectionAggregate;
using Together.Activity.Infrastructure.Data;
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
    }
}
