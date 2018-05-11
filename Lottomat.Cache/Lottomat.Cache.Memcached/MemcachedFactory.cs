using Enyim.Caching;

namespace Lottomat.Cache.Memcached
{
    public class MemcachedFactory
    {
        private static MemcachedClient _cacheClient = null;
        private static readonly object _lock = new object();

        /// <summary>
        /// 初始化Memcached对象
        /// </summary>
        /// <returns></returns>
        public static MemcachedClient GetMemcachedInstance()
        {
            if (_cacheClient == null)
            {
                lock (_lock)
                {
                    if (_cacheClient == null)
                    {
                        _cacheClient = new MemcachedClient();
                    }
                }
            }
            return _cacheClient;
        }
    }
}