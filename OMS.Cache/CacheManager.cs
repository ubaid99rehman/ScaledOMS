using Microsoft.Extensions.Caching.Memory;
using OMS.Core.Services.Cache;
using System;

namespace OMS.Cache
{
    public class CacheManager : ICacheManager
    {
        private readonly MemoryCache _cache;

        public CacheManager()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public T Get<T>(string key)
        {
            return _cache.TryGetValue(key, out var value) ? (T)value : default;
        }

        public void Set<T>(string key, T value, TimeSpan? expiration = null)
        {
            var cacheOptions = new MemoryCacheEntryOptions();
            if (expiration.HasValue)
            {
                cacheOptions.SetAbsoluteExpiration(expiration.Value);
            }

            _cache.Set(key, value, cacheOptions);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public bool ContainsKey(string key)
        {
            return _cache.TryGetValue(key, out _);
        }
    }

}
