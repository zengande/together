using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.UserGroup.API.Infrastructure.Repositories;

namespace Together.UserGroup.API.Infrastructure.Services
{
    public abstract class BaseService<T>
        where T : class, new()
    {
        private readonly IBaseRepository<T> _repository;
        protected BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<T> GetAsync(int Id)
        {
            return await _repository.GetAsync(Id);
        }

        public virtual T Update(T entity)
        {
            return _repository.Update(entity);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            return await _repository.AddAsync(entity);
        }

        public virtual bool Delete(T entity)
        {
            return _repository.Delete(entity);
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }

        public bool Existed(Func<T, bool> where)
        {
            return _repository.Existed(where);
        }
    }
}
