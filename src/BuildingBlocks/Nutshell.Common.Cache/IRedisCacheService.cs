using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nutshell.Common.Cache
{
    public interface IRedisCacheService
       : IDisposable
    {
        #region String 操作

        /// <summary>
        /// 设置 key 并保存字符串（如果 key 已存在，则覆盖值）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        bool StringSet(string redisKey, string redisValue, TimeSpan? expiry = null);

        /// <summary>
        /// 保存多个 Key-value
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        bool StringSet(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValuePairs);

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        string StringGet(string redisKey, TimeSpan? expiry = null);

        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        bool StringSet<T>(string redisKey, T redisValue, TimeSpan? expiry = null);

        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        T StringGet<T>(string redisKey, TimeSpan? expiry = null);

        #region async

        /// <summary>
        /// 保存一个字符串值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> StringSetAsync(string redisKey, string redisValue, TimeSpan? expiry = null);

        /// <summary>
        /// 保存一组字符串值
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        Task<bool> StringSetAsync(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValuePairs);

        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<string> StringGetAsync(string redisKey, string redisValue, TimeSpan? expiry = null);

        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> StringSetAsync<T>(string redisKey, T redisValue, TimeSpan? expiry = null);

        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<T> StringGetAsync<T>(string redisKey, TimeSpan? expiry = null);

        #endregion async

        #endregion String 操作

        #region Hash 操作

        /// <summary>
        /// 判断该字段是否存在 hash 中
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        bool HashExists(string redisKey, string hashField);

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        bool HashDelete(string redisKey, string hashField);

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        long HashDelete(string redisKey, IEnumerable<RedisValue> hashField);

        /// <summary>
        /// 在 hash 设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool HashSet(string redisKey, string hashField, string value);

        /// <summary>
        /// 在 hash 中设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        void HashSet(string redisKey, IEnumerable<HashEntry> hashFields);

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        RedisValue HashGet(string redisKey, string hashField);

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        RedisValue[] HashGet(string redisKey, RedisValue[] hashField, string value);

        /// <summary>
        /// 从 hash 返回所有的字段值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        IEnumerable<RedisValue> HashKeys(string redisKey);

        /// <summary>
        /// 返回 hash 中的所有值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        RedisValue[] HashValues(string redisKey);

        /// <summary>
        /// 在 hash 设定值（序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool HashSet<T>(string redisKey, string hashField, T value);

        /// <summary>
        /// 在 hash 中获取值（反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        T HashGet<T>(string redisKey, string hashField);

        #region async
        /// <summary>
        /// 判断该字段是否存在 hash 中
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<bool> HashExistsAsync(string redisKey, string hashField);

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<bool> HashDeleteAsync(string redisKey, string hashField);

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<long> HashDeleteAsync(string redisKey, IEnumerable<RedisValue> hashField);

        /// <summary>
        /// 在 hash 设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> HashSetAsync(string redisKey, string hashField, string value);

        /// <summary>
        /// 在 hash 中设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        Task HashSetAsync(string redisKey, IEnumerable<HashEntry> hashFields);

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<RedisValue> HashGetAsync(string redisKey, string hashField);

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<IEnumerable<RedisValue>> HashGetAsync(string redisKey, RedisValue[] hashField, string value);

        /// <summary>
        /// 从 hash 返回所有的字段值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<IEnumerable<RedisValue>> HashKeysAsync(string redisKey);

        /// <summary>
        /// 返回 hash 中的所有值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<IEnumerable<RedisValue>> HashValuesAsync(string redisKey);

        /// <summary>
        /// 在 hash 设定值（序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> HashSetAsync<T>(string redisKey, string hashField, T value);

        /// <summary>
        /// 在 hash 中获取值（反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<T> HashGetAsync<T>(string redisKey, string hashField);

        #endregion async

        #endregion Hash 操作

        #region List 操作

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        string ListLeftPop(string redisKey);

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        string ListRightPop(string redisKey);

        /// <summary>
        /// 移除列表指定键上与该值相同的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        long ListRemove(string redisKey, string redisValue);

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        long ListRightPush(string redisKey, string redisValue);

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        long ListLeftPush(string redisKey, string redisValue);

        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回 0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        long ListLength(string redisKey);

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        IEnumerable<RedisValue> ListRange(string redisKey);

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        T ListLeftPop<T>(string redisKey);

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        T ListRightPop<T>(string redisKey);

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        long ListRightPush<T>(string redisKey, T redisValue);

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        long ListLeftPush<T>(string redisKey, T redisValue);

        #region List-async

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<string> ListLeftPopAsync(string redisKey);

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<string> ListRightPopAsync(string redisKey);

        /// <summary>
        /// 移除列表指定键上与该值相同的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListRemoveAsync(string redisKey, string redisValue);

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync(string redisKey, string redisValue);

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync(string redisKey, string redisValue);

        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回 0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<long> ListLengthAsync(string redisKey);

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> ListRangeAsync<T>(string redisKey);

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<T> ListLeftPopAsync<T>(string redisKey);

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<T> ListRightPopAsync<T>(string redisKey);

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync<T>(string redisKey, T redisValue);

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync<T>(string redisKey, T redisValue);

        #endregion List-async

        #endregion List 操作

        Task<bool> KeyDeleteAsync(string key);

        Task<bool> KeyListDeleteAsync(List<string> keys);
    }
}
