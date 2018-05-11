using System;
using Enyim.Caching;
using Enyim.Caching.Memcached;
using Lottomat.Utils.Date;

namespace Lottomat.Cache.Memcached
{
    public class Memcached : ICache
    {
        private static readonly MemcachedClient CacheClient = MemcachedFactory.GetMemcachedInstance();
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public T GetCache<T>(string cacheKey) where T : class
        {
            try
            {
                return (T)CacheClient.Get(cacheKey);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 写入缓存，过期时间默认10分钟
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="cacheKey"></param>
        public void WriteCache<T>(T value, string cacheKey) where T : class
        {
            //CacheClient.Store(StoreMode.Set, cacheKey, value);

            CacheClient.Store(Exists(cacheKey) ? StoreMode.Set : StoreMode.Replace, cacheKey, value, DateTimeHelper.Now.AddMinutes(10));
        }

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="cacheKey"></param>
        /// <param name="expireTime"></param>
        public void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class
        {
            //CacheClient.Store(StoreMode.Set, cacheKey, value, expireTime);

            CacheClient.Store(Exists(cacheKey) ? StoreMode.Set : StoreMode.Replace, cacheKey, value, expireTime);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        public void RemoveCache(string cacheKey)
        {
            if (cacheKey.Equals("All"))
            {
                this.RemoveCache();
            }
            else
            {
                CacheClient.Remove(cacheKey);
            }
        }

        /// <summary>
        /// 移除所有缓存
        /// </summary>
        public void RemoveCache()
        {
            CacheClient.FlushAll();
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static bool Exists(string key)
        {
            return CacheClient.Get(key) != null;
        }
    }
}