using System;
using Together.Activity.Domain.AggregatesModel.CategoryAggregate;
using Together.Activity.Domain.SeedWork;
using Together.Activity.Infrastructure.Data;

namespace Together.Activity.Infrastructure.Repositories
{
    public class CategoryRepository
        : ICategoryRepository
    {
        private ActivityDbContext _context;
        public CategoryRepository(ActivityDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IUnitOfWork UnitOfWork => _context;
    }
}
