using System;
using System.Linq;
using System.Runtime.Caching;
using AweSamNet.Common.Configuration;
using NetMemoryCache = System.Runtime.Caching.MemoryCache;

namespace AweSamNet.Common.Caching
{   
    public class MemoryCache : ICache
    {
        private readonly IConfiguration _configuration;

        public MemoryCache(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T GetOrAdd<T>(string key, Func<T> setter, TimeSpan? expiration)
        {
            var value = NetMemoryCache.Default.AddOrGetExisting(key, setter(), new CacheItemPolicy()
            {
                SlidingExpiration = expiration.HasValue && expiration != TimeSpan.Zero ? expiration.Value : _configuration.DefaultCacheExpiration
            });

            return value != null ? (T)value : (T)NetMemoryCache.Default.Get(key);
        }

        public void Remove(string key)
        {
            NetMemoryCache.Default.Remove(key);
        }

        public void RemoveAll(string keyContains)
        {
            var keys = NetMemoryCache.Default.Where(x => x.Key.Contains(keyContains)).Select(x => x.Key);

            foreach (var key in keys)
            {
                NetMemoryCache.Default.Remove(key);
            }
        }
    }
}
