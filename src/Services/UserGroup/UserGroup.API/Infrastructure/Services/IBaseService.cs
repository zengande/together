using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.UserGroup.API.Infrastructure.Services
{
    public interface IBaseService<T>
        where T : class, new()
    {
        Task<T> GetAsync(int Id);
        T Update(T entity);
        Task<T> AddAsync(T entity);
        bool Delete(T entity);
        Task<bool> SaveChangesAsync();
        bool Existed(Func<T, bool> where);
    }
}
