using System;
using System.Collections.Generic;
using System.Text;
using Together.Activity.Domain.AggregatesModel.CatalogAggregate;
using Together.Activity.Infrastructure.EntityFrameworkCore;
using Together.BuildingBlocks.Domain;

namespace Together.Activity.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public IUnitOfWork UnitOfWork => _dbContext;

        private readonly ActivityDbContext _dbContext;
        public CategoryRepository(ActivityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Category Add(Category entity)
        {
            return _dbContext.Add(entity).Entity;
        }
    }
}
