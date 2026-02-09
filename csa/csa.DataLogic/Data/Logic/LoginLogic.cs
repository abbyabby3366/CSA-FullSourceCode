using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

using csa.Data;
using csa.Data.Cache;
using csa.Data.EntityModel;
using csa.Data.Library;
using csa.Library;
using csa.Model;

namespace csa.Data.Logic
{
    public class LoginLogic : BaseLogic
    {
        /// <summary>
        /// Authenticate admin login
        /// </summary>
        /// <param name="UserName">UserName</param>
        /// <param name="Password">Password</param>
        /// <param name="RespArgs">Respond argument - [Return] `LoginAdminModel` IF success ELSE `Null`</param>
        /// <returns>[Return] Login request boolean status</returns>
        public static bool LoginAuthByAdmin(string UserName, string Password, out RespArgs<LoginAdminModel> RespObj)
        {
            bool retVal = false;

            //default output variable
            RespObj = new RespArgs<LoginAdminModel> { Error = true };
            
            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `v_user_vault`
                    v_user_vault vUsrVaultExist = db.v_user_vault.Where(w =>
                        (w.AccountType & (int)AccountType.MEMBER) == (int)AccountType.MEMBER &&
                        (w.UserName == UserName || w.Email == UserName)).FirstOrDefault();

                    if (vUsrVaultExist == null)
                    {
                        RespObj.Error = true;
                        RespObj.Code = (int)ErrorCode.DATA_NOT_FOUND;
                        RespObj.Message = "Invalid user";

                        return false;
                    }

                    //if (vUsrVaultExist.Status != (int)UserStatus.ACTIVE)
                    //{
                    //    RespObj.Error = true;
                    //    RespObj.Code = (int)ErrorCode.INACTIVE_ACCOUNT;
                    //    RespObj.Message = "Inactive user";

                    //    return false;
                    //}

                    if (Utils.HashHelper.SHA512Hash(string.Concat(Password, vUsrVaultExist.Salt)) != vUsrVaultExist.Password)
                    {
                        RespObj.Error = true;
                        RespObj.Code = (int)ErrorCode.INVALID_CREDENTIAL;
                        RespObj.Message = "Invalid password";

                        return false;
                    }

                    DateTime curr = DateTime.UtcNow;

                    Guid usrSession_newId = Guid.NewGuid();
                    Guid loginLog_newId = Guid.NewGuid();

                    string ipAddress = Utils.Utilities.GetIPAddress();
                    string browser = Utils.Utilities.GetRequestBrowser();
                    
                    //get `user`
                    user userExist = db.users
                        .Include("ImageData")
                        .Where(w => w.Id == vUsrVaultExist.Id)
                        .FirstOrDefault();

                    if (userExist == null)
                    {
                        RespObj.Error = true;
                        RespObj.Code = (int)ErrorCode.DATA_NOT_FOUND;
                        RespObj.Message = "System error dependency userData not found";

                        return false;
                    }

                    //get max `LoginDateTime`
                    login_log loginLogExist = db.login_log
                        .Where(w => w.UserId == userExist.Id)
                        .OrderByDescending(o => o.LoginDateTime)
                        .FirstOrDefault();

                    //get ALL existing session of current login user
                    IEnumerable<user_session> sessionListExist = db.user_session
                        .Where(w => w.UserId == userExist.Id);

                    //biz logic - NO concurent login

                    //clear memCached
                    sessionListExist.ToList().ForEach(e => SessionCache.DeleteSessionCache(e.Id));

                    //clear existing concurrent login session
                    db.user_session.RemoveRange(sessionListExist);

                    //memCached store - KEY: sessionId; VAL: userId
                    SessionCache.SetLongLiveSessionCache(usrSession_newId, new AdminSessionCacheModel { UserId = userExist.Id });

                    //register `user_session`
                    user_session usrSession_new = new user_session
                    {
                        Id = usrSession_newId,
                        UserId = vUsrVaultExist.Id,
                        IPAddress = ipAddress,
                        LastAccess = curr
                    };

                    //add new session
                    db.user_session.Add(usrSession_new);

                    //log `login_log`
                    login_log loginLog_new = new login_log
                    {
                        Id = loginLog_newId,
                        UserId = vUsrVaultExist.Id,
                        IPAddress = ipAddress,
                        Browser = browser,
                        LoginDateTime = curr,
                    };

                    //add new loginLog
                    db.login_log.Add(loginLog_new);

                    db.SaveChanges();

                    //structure respond object
                    LoginAdminModel loginUser = new LoginAdminModel
                    {
                        ASId = usrSession_newId,
                        UserId = userExist.Id,
                        UserName = userExist.UserName,
                        Email = userExist.Email,
                        FirstName = userExist.FirstName,
                        LastName = userExist.LastName,
                        ImageData = (userExist.ImageData == null) ? null : new FileDataModel
                        { Id = userExist.ImageData.Id, Extension = userExist.ImageData.Extension },
                        LastLogin = (loginLogExist == null) ? DateTime.MinValue : loginLogExist.LoginDateTime,
                        AccountType = userExist.AccountType,
                        Status = userExist.Status
                    };

                    //assign output variable
                    RespObj = new RespArgs<LoginAdminModel>
                    {
                        Error = false,
                        Code = (int)ErrorCode.OK,
                        Message = "Successful",
                        ObjVal = loginUser
                    };

                    retVal = true;
                }
            }
            catch
            { }
            
            return retVal;
        }

        /// <summary>
        /// Authenticate member login
        /// </summary>
        /// <param name="UserName">UserName</param>
        /// <param name="Password">Password</param>
        /// <param name="RespArgs">Respond argument - [Return] `LoginMemberModel` IF success ELSE `Null`</param>
        /// <returns>[Return] Login request boolean status</returns>
        public static bool LoginAuthByCustomer(string UserName, string Password, out RespArgs<LoginMemberModel> RespObj)
        {
            bool retVal = false;

            //default output variable
            RespObj = new RespArgs<LoginMemberModel> { Error = true };

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `v_user_vault`
                    v_user_vault vUsrVaultExist = db.v_user_vault.Where(w =>
                        (w.AccountType & (int)AccountType.MEMBER) == (int)AccountType.MEMBER &&
                        (w.UserName == UserName || w.Email == UserName)).FirstOrDefault();

                    if (vUsrVaultExist == null)
                    {
                        RespObj.Error = true;
                        RespObj.Code = (int)ErrorCode.DATA_NOT_FOUND;
                        RespObj.Message = "Invalid user";

                        return false;
                    }

                    //if (vUsrVaultExist.Status != (int)UserStatus.ACTIVE)
                    //{
                    //    RespObj.Error = true;
                    //    RespObj.Code = (int)ErrorCode.INACTIVE_ACCOUNT;
                    //    RespObj.Message = "Inactive user";

                    //    return false;
                    //}

                    if (Utils.HashHelper.SHA512Hash(string.Concat(Password, vUsrVaultExist.Salt)) != vUsrVaultExist.Password)
                    {
                        RespObj.Error = true;
                        RespObj.Code = (int)ErrorCode.INVALID_CREDENTIAL;
                        RespObj.Message = "Invalid password";

                        return false;
                    }

                    DateTime curr = DateTime.UtcNow;

                    Guid usrSession_newId = Guid.NewGuid();
                    Guid loginLog_newId = Guid.NewGuid();

                    string ipAddress = Utils.Utilities.GetIPAddress();
                    string browser = Utils.Utilities.GetRequestBrowser();

                    //get `user`
                    user userExist = db.users
                        .Include("ImageData")
                        .Where(w => w.Id == vUsrVaultExist.Id)
                        .FirstOrDefault();

                    ////get `member`
                    //customer customerExist = db.customers
                    //    .Where(w => w.UserId == userExist.Id)
                    //    .FirstOrDefault();

                    if (userExist == null)
                    {
                        RespObj.Error = true;
                        RespObj.Code = (int)ErrorCode.DATA_NOT_FOUND;
                        RespObj.Message = "System error dependency userData not found";

                        return false;
                    }

                    //if (customerExist == null)
                    //{
                    //    RespObj.Error = true;
                    //    RespObj.Code = (int)ErrorCode.DATA_NOT_FOUND;
                    //    RespObj.Message = "System error dependency customerData not found";

                    //    return false;
                    //}

                    //get max `LoginDateTime`
                    login_log loginLogExist = db.login_log
                        .Where(w => w.UserId == userExist.Id)
                        .OrderByDescending(o => o.LoginDateTime)
                        .FirstOrDefault();

                    //get ALL existing session of current login user
                    IEnumerable<user_session> sessionListExist = db.user_session
                        .Where(w => w.UserId == userExist.Id);

                    //biz logic - NO concurent login

                    //clear memCached
                    sessionListExist.ToList().ForEach(e => SessionCache.DeleteSessionCache(e.Id));

                    //clear existing concurrent login session
                    db.user_session.RemoveRange(sessionListExist);

                    //memCached store - KEY: sessionId; VAL: userId
                    //SessionCache.SetLongLiveSessionCache(usrSession_newId, new CustomerSessionCacheModel { UserId = userExist.Id, CustomerId = customerExist.Id });
                    SessionCache.SetLongLiveSessionCache(usrSession_newId, new CustomerSessionCacheModel { UserId = userExist.Id, CustomerId = Guid.Empty });

                    //register `user_session`
                    user_session usrSession_new = new user_session
                    {
                        Id = usrSession_newId,
                        UserId = vUsrVaultExist.Id,
                        IPAddress = ipAddress,
                        LastAccess = curr
                    };

                    //add new session
                    db.user_session.Add(usrSession_new);

                    //log `login_log`
                    login_log loginLog_new = new login_log
                    {
                        Id = loginLog_newId,
                        UserId = vUsrVaultExist.Id,
                        IPAddress = ipAddress,
                        Browser = browser,
                        LoginDateTime = curr,
                    };

                    //add new loginLog
                    db.login_log.Add(loginLog_new);

                    db.SaveChanges();

                    //structure respond object
                    LoginMemberModel loginUser = new LoginMemberModel
                    {
                        USId = usrSession_newId,
                        UserId = userExist.Id,
                        UserName = userExist.UserName,
                        Email = userExist.Email,
                        FirstName = userExist.FirstName,
                        LastName = userExist.LastName,
                        ImageData = (userExist.ImageData == null) ? null : new FileDataModel
                        { Id = userExist.ImageData.Id, Extension = userExist.ImageData.Extension },
                        LastLogin = (loginLogExist == null) ? DateTime.MinValue : loginLogExist.LoginDateTime,
                        AccountType = userExist.AccountType,
                        //Wallet1Amount = customerExist.Wallet1Balance,
                        Status = userExist.Status
                    };

                    //assign output variable
                    RespObj = new RespArgs<LoginMemberModel>
                    {
                        Error = false,
                        Code = (int)ErrorCode.OK,
                        Message = "Successful",
                        ObjVal = loginUser
                    };

                    retVal = true;
                }
            }
            catch
            { }

            return retVal;
        }

        /// <summary>
        /// Account `logout`
        /// </summary>
        /// <param name="USid">User sessionId</param>
        /// <param name="RespObj">Respond argument - [Return] Logout `SessionId` IF `Success` ELSE `Null`</param>
        /// <returns>[Return] Logout request boolean status</returns>
        public static bool LogoutAccount(Guid? USid, out RespArgs<Guid?> RespObj)
        {
            bool retVal = false;

            //default output variable
            RespObj = new RespArgs<Guid?> { Error = true };

            if (USid == null || USid == Guid.Empty)
            {
                RespObj = new RespArgs<Guid?>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = Guid.Empty
                };

                return true;
            }

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get ALL existing session of current login user
                    IEnumerable<user_session> sessionListExist = db.user_session.Where(w => w.UserId == USid);

                    if (sessionListExist.Count() > 0)
                    {
                        //clear memCached
                        sessionListExist.ToList().ForEach(e => SessionCache.DeleteSessionCache(e.Id));

                        //clear existing concurrent login session
                        db.user_session.RemoveRange(sessionListExist);

                        db.SaveChanges();
                    }
                }

                //assign output variable
                RespObj = new RespArgs<Guid?>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = USid
                };

                retVal = true;
            }
            catch
            { }

            return retVal;
        }
    }
}
