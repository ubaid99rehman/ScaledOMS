using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Services.Cache
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan? expiration = null);
        void Remove(string key);
        bool ContainsKey(string key);
    }

}
