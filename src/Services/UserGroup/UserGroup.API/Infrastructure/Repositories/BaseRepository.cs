using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.UserGroup.API.Infrastructure.Data;

namespace Together.UserGroup.API.Infrastructure.Repositories
{
    public abstract class BaseRepository<T>
        where T : class, new()
    {
        private readonly UserGroupDbContext _context;
        protected BaseRepository(UserGroupDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> GetAsync(int Id)
        {
            var model = await _context.Set<T>()
                .FindAsync(Id);
            return model;
        }

        public virtual T Update(T entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return entity;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public virtual bool Delete(T entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            return true;
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool Existed(Func<T,bool> where)
        {
            return _context.Set<T>()
                .Any(where);
        }
    }
}
