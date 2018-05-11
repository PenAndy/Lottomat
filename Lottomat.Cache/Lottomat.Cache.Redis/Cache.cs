using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottomat.Util;
using Lottomat.Utils.Date;
using ServiceStack.Redis;

namespace Lottomat.Cache.Redis
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2016.04.28 10:45
    /// 描 述：定义缓存接口
    /// </summary>
    public class Redis : ICache
    {
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        public T GetCache<T>(string cacheKey) where T : class
        {
            return RedisCache.Get<T>(cacheKey);
        }

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        public void WriteCache<T>(T value, string cacheKey) where T : class
        {
            //RedisCache.Set(cacheKey, value);
            WriteCache(value, cacheKey, DateTimeHelper.Now.AddMinutes(10));
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="expireTime">到期时间</param>
        public void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class
        {
            RedisCache.Set(cacheKey, value, expireTime);
        }
        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public void RemoveCache(string cacheKey)
        {
            if (cacheKey.Equals("All"))
            {
                this.RemoveCache();
            }
            else
            {
                RedisCache.Remove(cacheKey);
            }
        }
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public void RemoveCache()
        {
            RedisCache.RemoveAll();
        }
    }
}
