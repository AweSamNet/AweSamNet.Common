using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AweSamNet.Common.Caching
{
    public interface ICache
    {
        T GetOrAdd<T>(string key, Func<T> setter, TimeSpan? expiration = null);
        T Get<T>(string key);
        void Remove(string key);
        void RemoveAll(string keyContains);
    }
}
