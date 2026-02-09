using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using csa.Data;
using csa.Data.Cache;
using csa.Data.EntityModel;
using csa.Library;
using csa.Model;

namespace csa.Data.Logic
{
    public class SettingLogic
    {
        /// <summary>
        /// Get general `setting` by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <returns>Respond argument - [Return] GeneralSettingByAdminModel</returns>
        public static RespArgs<GeneralSettingByAdminModel> GetGeneralSettingByAdmin(Guid? ASid)
        {
            RespArgs<GeneralSettingByAdminModel> retVal = new RespArgs<GeneralSettingByAdminModel> { Error = true };

            if (ASid == null)
            {
                retVal.Error = true;
                retVal.Code = (int)ErrorCode.SESSION_TIMEOUT;
                retVal.Message = "Session timeout";

                return retVal;
            }

            AdminSessionCacheModel session = SessionCache.GetSessionCacheByKey<AdminSessionCacheModel>(ASid.Value);

            if (session == null)
            {
                retVal.Error = true;
                retVal.Code = (int)ErrorCode.SESSION_TIMEOUT;
                retVal.Message = "Session timeout";

                return retVal;
            }

            //general `setting`
            int[] gnrlSettings = { (int)ConfigSettingType.MAINTENANCE_WEBSITE_CONFIG, (int)ConfigSettingType.ADMINISTRATOR_EMAIL };

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `setting` list
                    IEnumerable<setting> settingList_exist = db.settings
                        .Where(w => gnrlSettings.Contains(w.Ind));

                    if (settingList_exist.Count() != gnrlSettings.Count())
                    {
                        retVal.Error = true;
                        retVal.Code = (int)ErrorCode.INVALID_DATA;
                        retVal.Message = "Invalid configuration setting";

                        return retVal;
                    }

                    setting maintWebConfig = settingList_exist.Where(w => w.Ind == (int)ConfigSettingType.MAINTENANCE_MOBILE_CONFIG).FirstOrDefault();
                    setting adminEmailConfig = settingList_exist.Where(w => w.Ind == (int)ConfigSettingType.ADMINISTRATOR_EMAIL).FirstOrDefault();

                    //contruct output object
                    retVal = new RespArgs<GeneralSettingByAdminModel>
                    {
                        Error = false,
                        Code = (int)ErrorCode.OK,
                        Message = "Successful",
                        ObjVal = new GeneralSettingByAdminModel
                        {
                            MaintWebMode = (maintWebConfig.StrValue == "0") ? false : true,
                            MaintWebMsg = maintWebConfig.TextValue,
                            AdminEmail = adminEmailConfig.StrValue
                        }
                    };
                }
            }
            catch
            { }

            return retVal;
        }

        /// <summary>
        /// Edit general `setting` by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <param name="Data">EditGeneralSettingModel</param>
        /// <param name="RespObj">Respond argument - [Return] boolean 'true' IF success ELSE `Null`</param>
        /// <returns>[Return] Edit general-setting boolean status</returns>
        public static bool EditGeneralSettingByAdmin(Guid? ASid, EditGeneralSettingModel Data, out RespArgs<bool?> RespObj)
        {
            bool retVal = false;

            //default output variable
            RespObj = new RespArgs<bool?> { Error = true };

            if (ASid == null)
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.SESSION_TIMEOUT;
                RespObj.Message = "Session timeout";

                return retVal;
            }

            AdminSessionCacheModel session = SessionCache.GetSessionCacheByKey<AdminSessionCacheModel>(ASid.Value);

            if (session == null)
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.SESSION_TIMEOUT;
                RespObj.Message = "Session timeout";

                return false;
            }

            //validation

            if (Data == null)
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.REQUIRED_PARAMETER_NOT_SUPPLIED;
                RespObj.Message = "Required data not supplied";

                return false;
            }

            DateTime curr = DateTime.UtcNow;

            //general `setting`
            int[] gnrlSettings = { (int)ConfigSettingType.MAINTENANCE_WEBSITE_CONFIG, (int)ConfigSettingType.ADMINISTRATOR_EMAIL };

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `setting` list
                    IEnumerable<setting> settingList_exist = db.settings
                        .Where(w => gnrlSettings.Contains(w.Ind));

                    if (settingList_exist.Count() != gnrlSettings.Count())
                    {
                        RespObj.Error = true;
                        RespObj.Code = (int)ErrorCode.INVALID_DATA;
                        RespObj.Message = "Invalid configuration setting";

                        return false;
                    }

                    setting maintWebConfig = settingList_exist.Where(w => w.Ind == (int)ConfigSettingType.MAINTENANCE_MOBILE_CONFIG).FirstOrDefault();
                    setting adminEmailConfig = settingList_exist.Where(w => w.Ind == (int)ConfigSettingType.ADMINISTRATOR_EMAIL).FirstOrDefault();

                    if (maintWebConfig != null)
                    {
                        //update `maintWebConfig`
                        maintWebConfig.StrValue = (Data.MaintWebMode) ? "1" : "0";
                        maintWebConfig.TextValue = Data.MaintWebMsg;
                    }

                    if (adminEmailConfig != null)
                    {
                        //update `adminEmailConfig`
                        adminEmailConfig.StrValue = Data.AdminEmail;
                    }

                    db.SaveChanges();

                    retVal = true;
                }

                //construct output object'
                RespObj = new RespArgs<bool?>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = true
                };
            }
            catch
            { }

            if (retVal)
            {
                //reset `general` setting memCached
                GeneralSettingSessionCache.DeleteSessionCache();
            }

            return retVal;
        }

        //------------------------------------------------------------------------------------------------

        /// <summary>
        /// Set `Maintenance-mode` by anonymous
        /// </summary>
        /// <param name="IsMaintenanceMode">GlobalBoolean</param>
        /// <param name="RespObj">Respond argument - [Return] boolean 'true' IF success ELSE `Null`</param>
        /// <returns>[Return] Set `maintenance-mode` boolean status</returns>
        public static bool SetMaintenanceModeByAnonymous(GlobalBoolean IsMaintenanceMode, out RespArgs<bool?> RespObj)
        {
            bool retVal = false;

            //default output variable
            RespObj = new RespArgs<bool?> { Error = true };

            //biz logic :- anonymous can call this function

            //general `setting`
            int[] gnrlSettings = { (int)ConfigSettingType.MAINTENANCE_WEBSITE_CONFIG };

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `setting` list
                    IEnumerable<setting> settingList_exist = db.settings
                        .Where(w => gnrlSettings.Contains(w.Ind));

                    if (settingList_exist.Count() != gnrlSettings.Count())
                    {
                        RespObj.Error = true;
                        RespObj.Code = (int)ErrorCode.INVALID_DATA;
                        RespObj.Message = "Invalid configuration setting";

                        return false;
                    }

                    setting maintWebConfig = settingList_exist.Where(w => w.Ind == (int)ConfigSettingType.MAINTENANCE_WEBSITE_CONFIG).FirstOrDefault();

                    if (maintWebConfig != null)
                    {
                        //update `maintWebConfig`
                        maintWebConfig.StrValue = $"{(int)IsMaintenanceMode}";
                    }

                    db.SaveChanges();

                    retVal = true;
                }

                //construct output object'
                RespObj = new RespArgs<bool?>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = true
                };
            }
            catch
            { }

            if (retVal)
            {
                //reset `general` setting memCached
                GeneralSettingSessionCache.DeleteSessionCache();
            }

            return retVal;
        }

        //================================================================================================

        /// <summary>
        /// Get general `setting` by system
        /// System fetch detail data into `MemCached`
        /// </summary>
        /// <returns>Respond argument - [Return] GeneralSettingByMemCacheModel</returns>
        public static RespArgs<GeneralSettingByMemCacheModel> GetGeneralSettingBySystem()
        {
            RespArgs<GeneralSettingByMemCacheModel> retVal = new RespArgs<GeneralSettingByMemCacheModel> { Error = true };

            //biz logic :- anonymous can call this function

            //general `setting`
            int[] gnrlSettings = { (int)ConfigSettingType.MAINTENANCE_WEBSITE_CONFIG };

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `setting` list
                    IEnumerable<setting> settingList_exist = db.settings
                        .Where(w => gnrlSettings.Contains(w.Ind));

                    if (settingList_exist.Count() != gnrlSettings.Count())
                    {
                        retVal.Error = true;
                        retVal.Code = (int)ErrorCode.INVALID_DATA;
                        retVal.Message = "Invalid configuration setting";

                        return retVal;
                    }

                    setting mobileMaintConfig = settingList_exist.Where(w => w.Ind == (int)ConfigSettingType.MAINTENANCE_WEBSITE_CONFIG).FirstOrDefault();

                    //contruct output object
                    retVal = new RespArgs<GeneralSettingByMemCacheModel>
                    {
                        Error = false,
                        Code = (int)ErrorCode.OK,
                        Message = "Successful",
                        ObjVal = new GeneralSettingByMemCacheModel
                        {
                            MaintWebMode = (mobileMaintConfig.StrValue == "0") ? false : true,
                            MaintWebMsg = mobileMaintConfig.TextValue
                        }
                    };
                }
            }
            catch (Exception ex)
            { }

            return retVal;
        }
    }
}
