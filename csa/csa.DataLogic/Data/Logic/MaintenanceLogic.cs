using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using csa.Data.Cache;
using csa.Library;
using csa.Model;

namespace csa.Data.Logic
{
    public class MaintenanceLogic
    {
        /// <summary>
        /// Get `website` maintenance status
        /// </summary>
        /// <returns>[Return] Is website under maintenance boolean status</returns>
        public static bool IsWebMaintenanceMode()
        {
            bool retVal = true;

            //sys logic :- by default set it to `IsMaintenance` equalTo 'True'

            try
            {
                GeneralSettingByMemCacheModel generalSettingCache = GeneralSettingSessionCache.GetSessionCacheByKey();

                if (generalSettingCache == null)
                {
                    //get general `setting`
                    RespArgs<GeneralSettingByMemCacheModel> retObj = SettingLogic.GetGeneralSettingBySystem();

                    if (retObj == null)
                    { return true; }

                    if (retObj.Error)
                    { return true; }

                    if (retObj.ObjVal == null)
                    { return true; }

                    //save into `memCached`
                    GeneralSettingSessionCache.SetSessionCache(retObj.ObjVal);

                    retVal = retObj.ObjVal.MaintWebMode;
                }
                else
                {
                    if (generalSettingCache == null)
                    { return true; }

                    retVal = generalSettingCache.MaintWebMode;
                }
            }
            catch
            { }

            return retVal;
        }

        /// <summary>
        /// Get `website` maintenance config info
        /// </summary>
        /// <param name="RespObj">Respond argument - [Return] GeneralSettingByMemCacheModel</param>
        /// <returns>[Return] Get request boolean status</returns>
        public static bool GetWebMaintenanceConfig(out RespArgs<GeneralSettingByMemCacheModel> RespObj)
        {
            bool retVal = false;

            //default output variable
            RespObj = new RespArgs<GeneralSettingByMemCacheModel> { Error = true };

            try
            {
                GeneralSettingByMemCacheModel generalSettingCache = GeneralSettingSessionCache.GetSessionCacheByKey();

                if (generalSettingCache == null)
                {
                    //get general `setting`
                    RespArgs<GeneralSettingByMemCacheModel> retObj = SettingLogic.GetGeneralSettingBySystem();

                    if (retObj == null)
                    { return false; }

                    if (retObj.Error)
                    { return false; }

                    if (retObj.ObjVal == null)
                    { return false; }

                    //save into `memCached`
                    GeneralSettingSessionCache.SetSessionCache(retObj.ObjVal);

                    //construct output object
                    RespObj = retObj;
                }
                else
                {
                    if (generalSettingCache == null)
                    { return false; }
                }

                retVal = true;
            }
            catch
            { }

            return retVal;
        }
    }
}
