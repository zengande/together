using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Domain.AggregatesModel.CatalogAggregate;
using Together.Activity.Infrastructure.Data;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Infrastructure.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        public IUnitOfWork UnitOfWork => _dbContext;

        private readonly ActivityDbContext _dbContext;
        public CatalogRepository(ActivityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Catalog Add(Catalog entity)
        {
            return _dbContext.Add(entity).Entity;
        }
    }
}
