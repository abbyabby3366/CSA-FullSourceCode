using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Enyim.Caching;
using Enyim.Caching.Memcached;
using Newtonsoft.Json;

namespace csa.Data.Library
{
    public class MemCachedHelper
    {
        private object _lock = new object();

        public T Get<T>(string CacheName)
        {
            lock (_lock)
            {
                using (MemcachedClient client = new MemcachedClient())
                {
                    try
                    {
                        string tmp = client.Get<string>(CacheName);

                        if (string.IsNullOrEmpty(tmp))
                        { return default(T); }
                        else
                        { return JsonConvert.DeserializeObject<T>(tmp); }
                    }
                    catch { }

                    return default(T);
                }
            }
        }

        public bool Store<T>(string CacheName, T Value)
        {
            bool retVal = false;

            lock (_lock)
            {
                using (MemcachedClient client = new MemcachedClient())
                {
                    try
                    {
                        string tmp = JsonConvert.SerializeObject((T)Value);

                        retVal = client.Store(StoreMode.Set, CacheName, tmp);
                    }
                    catch
                    { }
                }
            }

            return retVal;
        }

        public bool Store<T>(string CacheName, T Value, DateTime ExpiredAt)
        {
            bool retVal = false;

            lock (_lock)
            {
                using (MemcachedClient client = new MemcachedClient())
                {
                    try
                    {
                        string tmp = JsonConvert.SerializeObject((T)Value);

                        retVal = client.Store(StoreMode.Set, CacheName, tmp, ExpiredAt);
                    }
                    catch
                    { }
                }
            }

            return retVal;
        }

        public bool Store<T>(string CacheName, T Value, TimeSpan ValidFor)
        {
            bool retVal = false;

            lock (_lock)
            {
                using (MemcachedClient client = new MemcachedClient())
                {
                    try
                    {
                        string tmp = JsonConvert.SerializeObject((T)Value);

                        retVal = client.Store(StoreMode.Set, CacheName, tmp, ValidFor);
                    }
                    catch
                    { }
                }
            }

            return retVal;
        }

        public bool Remove(string CacheName)
        {
            bool retVal = false;

            lock (_lock)
            {
                using (MemcachedClient client = new MemcachedClient())
                {
                    retVal = client.Remove(CacheName);
                }
            }

            return retVal;
        }

        public void FlushAll()
        {
            lock (_lock)
            {
                using (MemcachedClient client = new MemcachedClient())
                {
                    client.FlushAll();
                }
            }
        }
    }
}
