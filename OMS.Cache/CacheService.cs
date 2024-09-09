using OMS.Core.Services;
using OMS.Core.Services.Cache;
using System;

namespace OMS.Cache
{
    public class CacheService : ICacheService
    {
        private readonly ICacheManager _cacheManager;

        public CacheService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public T Get<T>(string key)
        {
            return _cacheManager.Get<T>(key);
        }

        public void Set<T>(string key, T value, TimeSpan? expiration = null)
        {
            _cacheManager.Set(key, value, expiration);
        }

        public void Remove(string key)
        {
            _cacheManager.Remove(key);
        }

        public bool ContainsKey(string key)
        {
            return _cacheManager.ContainsKey(key);
        }
    }

}
