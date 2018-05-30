using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nutshell.Common.Cache
{
    public interface ICacheService : IDisposable
    {
        #region 存在判断
        bool Exists(string key);
        Task<bool> ExistsAsync(string key); 
        #endregion

        #region 添加
        bool Add(string key, object value);
        Task<bool> AddAsync(string key, object value);
        bool Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);
        Task<bool> AddAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);
        bool Add(string key, object value, TimeSpan expiresIn, bool isSliding = false);
        Task<bool> AddAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false);
        #endregion

        #region 删除
        bool Remove(string key);
        Task<bool> RemoveAsync(string key);
        void RemoveAll(IEnumerable<string> keys);
        Task RemoveAllAsync(IEnumerable<string> keys);
        #endregion

        #region 获取
        T Get<T>(string key) where T : class;
        Task<T> GetAsync<T>(string key) where T : class;
        object Get(string key);
        Task<object> GetAsync(string key);
        IDictionary<string, object> GetAll(IEnumerable<string> keys);
        Task<IDictionary<string, object>> GetAllAsync(IEnumerable<string> keys);
        #endregion

        #region 修改
        bool Replace(string key, object value);
        Task<bool> ReplaceAsync(string key, object value);
        bool Replace(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);
        Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);
        bool Replace(string key, object value, TimeSpan expiresIn, bool isSliding = false);
        Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false); 
        #endregion
    }
}
