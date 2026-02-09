using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using csa.Data.Library;
using csa.Model;

namespace csa.Data.Cache
{
    public class SessionCache
    {
        public static T GetSessionCacheByKey<T>(Guid SessionId)
        {
            MemCachedHelper client = new MemCachedHelper();

            return client.Get<T>($"session_{SessionId}");
        }

        public static bool SetSessionCache<T>(Guid SessionId, T SessionCacheObj)
        {
            bool retVal = false;

            DateTime curr = DateTime.Today;
            DateTime expired = new DateTime(curr.Year, curr.Month, curr.Day, 23, 59, 59);

            MemCachedHelper client = new MemCachedHelper();

            //retVal = client.Store<T>($"session_{SessionId}", SessionCacheObj, new TimeSpan(0, 30, 0));
            retVal = client.Store<T>($"session_{SessionId}", SessionCacheObj, expired);

            return retVal;
        }

        public static bool SetLongLiveSessionCache<T>(Guid SessionId, T SessionCacheObj)
        {
            bool retVal = false;

            DateTime curr = DateTime.Today;
            DateTime expired = new DateTime(curr.Year, curr.Month, curr.Day, 23, 59, 59).AddMonths(3);

            MemCachedHelper client = new MemCachedHelper();

            retVal = client.Store<T>($"session_{SessionId}", SessionCacheObj, expired);

            return retVal;
        }

        public static bool DeleteSessionCache(Guid SessionId)
        {
            bool retVal = false;

            MemCachedHelper client = new MemCachedHelper();

            retVal = client.Remove($"session_{SessionId}");

            return retVal;
        }
    }

    //------------------------------------------------------------------------------------------------

    public class SessionBaseCacheModel
    {
        public Guid UserId { get; set; }
    }

    public class CustomerSessionCacheModel : SessionBaseCacheModel
    {
        public Guid CustomerId { get; set; }
    }

    public class AdminSessionCacheModel : SessionBaseCacheModel
    {

    }

    //================================================================================================

    public class GeneralSettingSessionCache
    {
        public static GeneralSettingByMemCacheModel GetSessionCacheByKey()
        {
            MemCachedHelper client = new MemCachedHelper();

            return client.Get<GeneralSettingByMemCacheModel>("general_setting");
        }

        public static bool SetSessionCache<GeneralSettingByMemCacheModel>(GeneralSettingByMemCacheModel SessionCacheObj)
        {
            bool retVal = false;

            DateTime curr = DateTime.Today;
            DateTime expired = new DateTime(curr.Year, curr.Month, curr.Day, 23, 59, 59).AddDays(1);

            //due to normally maintenace will held-on SAME DAY ~23:59:59  or NEXT DAY ~00:00:00
            //so, for safety purpose make it extend to NEXT DAY

            MemCachedHelper client = new MemCachedHelper();

            retVal = client.Store("general_setting", SessionCacheObj, expired);

            return retVal;
        }

        public static bool DeleteSessionCache()
        {
            bool retVal = false;

            MemCachedHelper client = new MemCachedHelper();

            retVal = client.Remove("general_setting");

            return retVal;
        }
    }

    //------------------------------------------------------------------------------------------------
}
