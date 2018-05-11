using Lottomat.Cache.Memcached;
using System;

namespace Lottomat.Cache.Factory
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.11.9 10:45
    /// 描 述：缓存工厂类
    /// </summary>
    public class CacheFactory
    {
        private static readonly string CacheType;

        private CacheFactory(){}
        static CacheFactory()
        {
            CacheType = Lottomat.Util.ConfigHelper.GetValue("CacheType");
        }

        /// <summary>
        /// 定义通用的Repository
        /// </summary>
        /// <returns></returns>
        public static ICache Cache()
        {
            //modify by 赵轶 20160816 修改为支持Redis
            switch (CacheType)
            {
                case "Redis":
                    return new Redis.Redis();
                case "WebCache":
                    return new Cache();
                case "Memcached":
                    return new Memcached.Memcached();
                default:
                    return new Cache();
            }
        }
    }
}
