using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AweSamNet.Common.Interfaces;
using System.Runtime.Caching;
using NetMemoryCache = System.Runtime.Caching.MemoryCache;

namespace AweSamNet.Common.Caching
{   
    public class MemoryCache : ICache
    {

        public T GetOrAdd<T>(string key, Func<T> setter, TimeSpan expiration)
        {
            return (T)NetMemoryCache.Default.AddOrGetExisting(key, setter(), new CacheItemPolicy() { SlidingExpiration = expiration });
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
