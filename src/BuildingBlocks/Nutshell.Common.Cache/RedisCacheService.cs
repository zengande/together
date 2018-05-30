using Microsoft.Extensions.Caching.Redis;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutshell.Common.Cache
{
    public class RedisCacheService
        : ICacheService
    {
        protected IDatabase _database;
        private readonly ConnectionMultiplexer _connection;
        private readonly string _instance;
        public RedisCacheService(RedisCacheOptions options, int database = 0)
        {
            _connection = ConnectionMultiplexer.Connect(options.Configuration);
            _database = _connection.GetDatabase(database);
            _instance = options.InstanceName;
        }

        public string GetKeyForRedis(string key)
        {
            return $"{_instance}:{key}";
        }

        #region 添加
        public bool Add(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _database.StringSet(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
        }

        public bool Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _database.StringSet(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), expiressAbsoulte);
        }

        public bool Add(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _database.StringSet(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), expiresIn);
        }

        public async Task<bool> AddAsync(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            return await _database.StringSetAsync(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
        }

        public async Task<bool> AddAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            return await _database.StringSetAsync(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), expiressAbsoulte);
        }

        public async Task<bool> AddAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            return await _database.StringSetAsync(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), expiresIn);
        }

        #endregion

        #region 判断存在
        public bool Exists(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _database.KeyExists(GetKeyForRedis(key));
        }

        public async Task<bool> ExistsAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            return await _database.KeyExistsAsync(GetKeyForRedis(key));
        }
        #endregion

        #region 获取
        public T Get<T>(string key) where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            var value = _database.StringGet(GetKeyForRedis(key));
            if (!value.HasValue)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        public object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            var value = _database.StringGet(GetKeyForRedis(key));
            return value;
        }

        public IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }
            var dict = new Dictionary<string, object>();
            keys.ToList().ForEach(item => dict.Add(item, Get(GetKeyForRedis(item))));

            return dict;
        }

        public async Task<IDictionary<string, object>> GetAllAsync(IEnumerable<string> keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }
            var dict = new Dictionary<string, object>();
            foreach (var key in keys)
            {
                dict.Add(key, await GetAsync(GetKeyForRedis(key)));
            }

            return dict;
        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            var value = await _database.StringGetAsync(GetKeyForRedis(key));
            if (!value.HasValue)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<object> GetAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            var value = await _database.StringGetAsync(GetKeyForRedis(key));
            return value;
        }
        #endregion

        #region 移除
        public bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _database.KeyDelete(key);
        }

        public void RemoveAll(IEnumerable<string> keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }
            keys.ToList().ForEach(key => _database.KeyDelete(key));
        }

        public async Task RemoveAllAsync(IEnumerable<string> keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }
            foreach (var key in keys)
            {
                await _database.KeyDeleteAsync(key);
            }
        }

        public async Task<bool> RemoveAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            return await _database.KeyDeleteAsync(key);
        }

        #endregion

        #region 更新
        public bool Replace(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (Exists(key))
            {
                if (!Remove(key))
                {
                    return false;
                }
            }
            return Add(key, value);
        }

        public bool Replace(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (Exists(key))
                if (!Remove(key))
                    return false;

            return Add(key, value, expiresSliding, expiressAbsoulte);
        }

        public bool Replace(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (Exists(key))
                if (!Remove(key)) return false;

            return Add(key, value, expiresIn, isSliding);
        }

        public async Task<bool> ReplaceAsync(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (await ExistsAsync(key))
            {
                bool result = await RemoveAsync(key);
                if (!result)
                {
                    return false;
                }
            }
            return await AddAsync(key, value);
        }

        public async Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (await ExistsAsync(key))
                if (!await RemoveAsync(key))
                    return false;

            return await AddAsync(key, value, expiresSliding, expiressAbsoulte);
        }

        public async Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (await ExistsAsync(key))
                if (!await RemoveAsync(key)) return false;

            return await AddAsync(key, value, expiresIn, isSliding);
        }
        #endregion

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
