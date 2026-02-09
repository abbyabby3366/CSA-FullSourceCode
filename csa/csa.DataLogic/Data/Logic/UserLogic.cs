using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

using log4net;
using MySql.Data.MySqlClient;

using csa.DataLogic.Languages;
using csa.Data.Cache;
using csa.Data.EntityModel;
using csa.Data.Library;
using csa.Library;
using csa.Model;
using csa.Model.SPModel;

namespace csa.Data.Logic
{
    public class UserLogic : BaseLogic
    {
        /// <summary>
        /// User registration by self
        /// </summary>
        /// <param name="UserRegisterObj">User registration parameters</param>
        /// <param name="RespObj">Respond argument - [Return] `UserId` IF success ELSE `Null`</param>
        /// <returns>[Return] User registration boolean status</returns>
        public static bool UserRegistration(UserRegistrationModel Data, out RespArgs<Guid?> RespObj)
        {
            bool retVal = false;

            //default output variable
            RespObj = new RespArgs<Guid?> { Error = true };

            //biz logic :- anonymous can call this function

            //validation

            //validate `password`
            if (!Utils.Validation.IsPassword(Data.Password))
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.INVALID_INPUT_FORMAT;
                RespObj.Message = Lang.ResourceManager.GetString("ErrInvalidPasswordFormat");

                return false;
            }

            if (string.IsNullOrWhiteSpace(Data.PhoneNo) || !Utils.Validation.IsPhoneNo(Data.PhoneNo))
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.INVALID_INPUT_FORMAT;
                RespObj.Message = "Invalid phone number";

                return false;
            }

            //overwrite

            Guid? countryId = null;

            if (Data.CountryId.HasValue)
            { countryId = Data.CountryId; }
            else
            { countryId = new Guid("082d9dd9-6787-4dc5-9858-80d3c65d2b05"); }

            string firstName = Data.FirstName ?? string.Empty;
            string lastName = Data.LastName ?? string.Empty;

            //string[] replacePhoneNoCharList = new string[] { "(", ")", "-", " " };

            //replacePhoneNoCharList.ToList().ForEach(o => Data.PhoneNo = Data.PhoneNo.Replace(o, string.Empty));


            Guid? user_newId = null;
            DateTime curr = DateTime.UtcNow;

            int retCode = 0;
            string retMsg = string.Empty;

            using (DataContext db = new DataContext())
            {
                try
                {
                    string salt = Utils.HashHelper.GenerateSalt();
                    string saltPassword = Utils.HashHelper.SHA512Hash(string.Concat(Data.Password, salt));

                    MySqlParameter[] param = new MySqlParameter[] {
                        new MySqlParameter() { ParameterName = "@i_guidRequestor", MySqlDbType = MySqlDbType.VarChar, IsNullable = true, Value = Guid.NewGuid() },
                        new MySqlParameter() { ParameterName = "@i_chvUserName", MySqlDbType = MySqlDbType.VarChar, Value = Data.UserName },
                        new MySqlParameter() { ParameterName = "@i_chvPassSalt", MySqlDbType = MySqlDbType.VarChar, Value = salt },
                        new MySqlParameter() { ParameterName = "@i_chvPassword", MySqlDbType = MySqlDbType.VarChar, Value = saltPassword },
                        new MySqlParameter() { ParameterName = "@i_chvFirstName", MySqlDbType = MySqlDbType.VarChar, Value = firstName },
                        new MySqlParameter() { ParameterName = "@i_chvLastName", MySqlDbType = MySqlDbType.VarChar, Value = lastName },
                        new MySqlParameter() { ParameterName = "@i_chvICNumber", MySqlDbType = MySqlDbType.VarChar, Value = string.Empty },
                        new MySqlParameter() { ParameterName = "@i_chvPassportNo", MySqlDbType = MySqlDbType.VarChar, Value = string.Empty },
                        new MySqlParameter() { ParameterName = "@i_dtmDateOfBirth", MySqlDbType = MySqlDbType.DateTime, IsNullable = true, Value = new DateTime(1900, 1, 1).ToString("yyyy-MM-dd HH:mm:ss") },
                        new MySqlParameter() { ParameterName = "@i_intGender", MySqlDbType = MySqlDbType.Int32, Value = (int)Gender.MALE },
                        new MySqlParameter() { ParameterName = "@i_guidNationality", MySqlDbType = MySqlDbType.VarChar, IsNullable = true, Value = null },
                        new MySqlParameter() { ParameterName = "@i_chvTimeZone", MySqlDbType = MySqlDbType.VarChar, Value = Constant.TIME_ZONE_ID },
                        new MySqlParameter() { ParameterName = "@i_chvEmail", MySqlDbType = MySqlDbType.VarChar, Value = Data.Email },
                        new MySqlParameter() { ParameterName = "@i_chvPhoneNo", MySqlDbType = MySqlDbType.VarChar, Value = Data.PhoneNo },
                        new MySqlParameter() { ParameterName = "@i_guidImageId", MySqlDbType = MySqlDbType.VarChar, IsNullable = true, Value = null },
                        new MySqlParameter() { ParameterName = "@i_chvAddress", MySqlDbType = MySqlDbType.VarChar, IsNullable = true, Value = null },
                        new MySqlParameter() { ParameterName = "@i_chvCity", MySqlDbType = MySqlDbType.VarChar, Value = null },
                        new MySqlParameter() { ParameterName = "@i_chvPostcode", MySqlDbType = MySqlDbType.VarChar, IsNullable = true, Value = null },
                        new MySqlParameter() { ParameterName = "@i_guidState", MySqlDbType = MySqlDbType.VarChar, IsNullable = true, Value = null },
                        new MySqlParameter() { ParameterName = "@i_chvState", MySqlDbType = MySqlDbType.VarChar, IsNullable = true, Value = null },
                        new MySqlParameter() { ParameterName = "@i_guidCountry", MySqlDbType = MySqlDbType.VarChar, Value = countryId },
                        //new MySqlParameter() { ParameterName = "@i_intStatus", MySqlDbType = MySqlDbType.Int32, Value = (int)UserStatus.ACTIVE },
                        new MySqlParameter() { ParameterName = "@i_guidRefAgentId", MySqlDbType = MySqlDbType.VarChar, IsNullable = true, Value = null },
                        new MySqlParameter() { ParameterName = "@i_dtmProcessDate", MySqlDbType = MySqlDbType.DateTime, Value = curr.ToString("yyyy-MM-dd HH:mm:ss") }
                    };

                    var result = db.Database.SqlQuery<SP_add_user_registration_Result>(
                        @"call sp_user_registration (@i_guidRequestor, @i_chvUserName, @i_chvPassSalt, @i_chvPassword, @i_chvFirstName, @i_chvLastName, @i_chvICNumber, 
                            @i_chvPassportNo, @i_dtmDateOfBirth, @i_intGender, @i_guidNationality, @i_chvTimeZone, @i_chvEmail, @i_chvPhoneNo, @i_guidImageId,
                            @i_chvAddress, @i_chvCity, @i_chvPostcode, @i_guidState, @i_chvState, @i_guidCountry, @i_intStatus, 
                            @i_guidRefAgentId, @i_dtmProcessDate)", param);

                    var retObj = result.FirstOrDefault();

                    if (retObj != null)
                    {
                        retCode = retObj.CodeResult;
                        retMsg = retObj.MsgResult;

                        retVal = retObj.IsSucceed;

                        if (retObj.IsSucceed)
                        {
                            //assign output value
                            user_newId = retObj.UserId;
                        }
                    }
                }
                catch
                { }

                //assign output variable
                RespObj = new RespArgs<Guid?>
                {
                    Error = (!retVal),
                    Code = retCode,
                    Message = retMsg,
                    ObjVal = user_newId
                };
            }

            //implement this `callback` to separate the `Email sending` from `Account create` and speed-up the page respond for `user UIX`

            if (retVal)
            {
                Action<EmailCallbackEventArgs<UserRegistrationModel>> callback = UserRegistrationEmailCallback;

                Guid tmpNewUsrId = user_newId ?? Guid.Empty;

                Task<bool> task = UserRegistrationSendEmail(Data, tmpNewUsrId, callback);
            }

            return retVal;
        }

        private static async Task<bool> UserRegistrationSendEmail(UserRegistrationModel Data, Guid NewUserId, Action<EmailCallbackEventArgs<UserRegistrationModel>> Callback)
        {
            bool retVal = false;

            if (Callback != null)
            {
                EmailCallbackEventArgs<UserRegistrationModel> args = new EmailCallbackEventArgs<UserRegistrationModel>
                {
                    NewUserId = NewUserId,
                    Data = Data,
                };

                await Task.Delay(500).ContinueWith(c =>
                {
                    //trigger callback
                    Callback(args);
                }).ConfigureAwait(false);
            }

            return retVal;
        }

        private static void UserRegistrationEmailCallback(EmailCallbackEventArgs<UserRegistrationModel> obj)
        {
            if (obj == null || obj.Data == null || obj.NewUserId == Guid.Empty)
            { return; }

            try
            {
                setting emailTemplateNewMemberCreation = null;
                setting emailTemplateAdminNewMemberCreation = null;
                setting adminEmail = null;

                using (DataContext db = new DataContext())
                {
                    //get `setting`
                    IQueryable<setting> settingExist = db.settings;

                    emailTemplateNewMemberCreation = settingExist.Where(w => w.Ind == (int)ConfigSettingType.EMAIL_NEW_MEMB_CREATION).FirstOrDefault();
                    emailTemplateAdminNewMemberCreation = settingExist.Where(w => w.Ind == (int)ConfigSettingType.EMAIL_ADMIN_NEW_MEMB_CREATION).FirstOrDefault();
                    adminEmail = settingExist.Where(w => w.Ind == (int)ConfigSettingType.ADMINISTRATOR_EMAIL).FirstOrDefault();
                }

                if (emailTemplateNewMemberCreation != null)
                {
                    string emailSubject = emailTemplateNewMemberCreation.StrValue;
                    string emailContent = emailTemplateNewMemberCreation.TextValue;

                    emailContent = emailContent.Replace("{Email}", obj.Data.Email);
                    //emailContent = emailContent.Replace("[Password]", obj.Data.Password);
                    emailContent = emailContent.Replace("{Firstname}", obj.Data.FirstName);
                    emailContent = emailContent.Replace("{Lastname}", obj.Data.LastName);

                    bool emailBool = false;

                    System.Net.Mail.MailAddress[] to = new System.Net.Mail.MailAddress[] { new System.Net.Mail.MailAddress(obj.Data.Email) };
                    System.Net.Mail.MailAddress[] cc = new System.Net.Mail.MailAddress[] { };
                    System.Net.Mail.MailAddress[] bcc = new System.Net.Mail.MailAddress[] { };
                    string[] attachments = new string[] { };

                    //send email - user
                    emailBool = GeneralHelper.SendEmail(emailSubject, emailContent, to, cc, bcc, attachments);

                    if (!emailBool)
                    { }

                    //email log
                    bool retEmailLogBool = MiscLogic.EmailLogging(emailBool, emailSubject, emailContent, to, cc, bcc, attachments,
                        EmailType: EmailLogType.MEMB_NEW_CREATION, TargetUser: obj.NewUserId, Creator: obj.NewUserId);
                }

                if (emailTemplateAdminNewMemberCreation != null && adminEmail != null)
                {
                    string emailSubject = emailTemplateAdminNewMemberCreation.StrValue;
                    string emailContent = emailTemplateAdminNewMemberCreation.TextValue;

                    emailContent = emailContent.Replace("{Email}", obj.Data.Email);
                    //emailContent = emailContent.Replace("[Password]", obj.Data.Password);
                    emailContent = emailContent.Replace("{Firstname}", obj.Data.FirstName);
                    emailContent = emailContent.Replace("{Lastname}", obj.Data.LastName);

                    //admin1.mail.com;admin2.mai.com;admin3.mail.com
                    string[] emails = adminEmail.StrValue.Split(';');

                    List<System.Net.Mail.MailAddress> receiverList = new List<System.Net.Mail.MailAddress>();

                    foreach (var item in emails)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        { receiverList.Add(new System.Net.Mail.MailAddress(item.Trim())); }
                    }

                    bool emailBool = false;

                    System.Net.Mail.MailAddress[] to = new System.Net.Mail.MailAddress[] { };
                    System.Net.Mail.MailAddress[] cc = new System.Net.Mail.MailAddress[] { };
                    System.Net.Mail.MailAddress[] bcc = new System.Net.Mail.MailAddress[] { };
                    string[] attachments = new string[] { };

                    if (receiverList.Count > 0)
                    {
                        //convert to `Array`
                        to = receiverList.ToArray();

                        //send email - administrator
                        emailBool = GeneralHelper.SendEmail(emailSubject, emailContent, to, cc, bcc, attachments);

                        if (!emailBool)
                        { }

                        //email log
                        bool retEmailLogBool = MiscLogic.EmailLogging(emailBool, emailSubject, emailContent, to, cc, bcc, attachments,
                            EmailType: EmailLogType.ADMIN_NEW_MEMB_CREATION, TargetUser: obj.NewUserId, Creator: obj.NewUserId);
                    }
                }
            }
            catch
            { }
        }

        //------------------------------------------------------------------------------------------------

        /// <summary>
        /// Customer user registration by admin
        /// </summary>
        /// <param name="CustomerRegistrationModel">Customer user registration parameters</param>
        /// <param name="RespObj">Respond argument - [Return] `UserId` IF success ELSE `Null`</param>
        /// <returns>[Return] User registration boolean status</returns>
        public static bool CustomerRegistrationByAdmin(CustomerRegistrationModel Data, out RespArgs<Guid?> RespObj)
        {
            bool retVal = false;

            //default output variable
            RespObj = new RespArgs<Guid?> { Error = true };

            //biz logic :- anonymous can call this function

            //validation

            //validate `password`
            if (!Utils.Validation.IsPassword(Data.Password))
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.INVALID_INPUT_FORMAT;
                RespObj.Message = Lang.ResourceManager.GetString("ErrInvalidPasswordFormat");

                return false;
            }

            if (string.IsNullOrWhiteSpace(Data.PhoneNo) || !Utils.Validation.IsPhoneNo(Data.PhoneNo))
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.INVALID_INPUT_FORMAT;
                RespObj.Message = "Invalid phone number";

                return false;
            }

            //overwrite

            Guid? countryId = null;
            Guid? stateId = null;
            string state = null;

            if (Data.CountryId.HasValue)
            { countryId = Data.CountryId; }
            else
            { countryId = new Guid("082d9dd9-6787-4dc5-9858-80d3c65d2b05"); }

            if (Data.StateId.HasValue)
            {
                stateId = Data.StateId;
                state = null;
            }
            else
            { state = Data.State; }

            string firstName = Data.FirstName ?? string.Empty;
            string lastName = Data.LastName ?? string.Empty;

            //string[] replacePhoneNoCharList = new string[] { "(", ")", "-", " " };

            //replacePhoneNoCharList.ToList().ForEach(o => Data.PhoneNo = Data.PhoneNo.Replace(o, string.Empty));

            int status = 0;

            int[] allowCustSts = { (int)MemberStatus.ACTIVE, (int)MemberStatus.INACTIVE };

            if (!allowCustSts.Contains(Data.Status))
            { status = (int)MemberStatus.INACTIVE; }
            else
            { status = Data.Status; }


            Guid? user_newId = null;
            DateTime curr = DateTime.UtcNow;

            int retCode = 0;
            string retMsg = string.Empty;

            using (DataContext db = new DataContext())
            {
                try
                {
                    string salt = Utils.HashHelper.GenerateSalt();
                    string saltPassword = Utils.HashHelper.SHA512Hash(string.Concat(Data.Password, salt));

                    MySqlParameter[] param = new MySqlParameter[] {
                        new MySqlParameter() { ParameterName = "@i_guidRequestor", MySqlDbType = MySqlDbType.VarChar, IsNullable = true, Value = Guid.NewGuid() },
                        new MySqlParameter() { ParameterName = "@i_chvUserName", MySqlDbType = MySqlDbType.VarChar, Value = Data.UserName },
                        new MySqlParameter() { ParameterName = "@i_chvPassSalt", MySqlDbType = MySqlDbType.VarChar, Value = salt },
                        new MySqlParameter() { ParameterName = "@i_chvPassword", MySqlDbType = MySqlDbType.VarChar, Value = saltPassword },
                        new MySqlParameter() { ParameterName = "@i_chvFirstName", MySqlDbType = MySqlDbType.VarChar, Value = firstName },
                        new MySqlParameter() { ParameterName = "@i_chvLastName", MySqlDbType = MySqlDbType.VarChar, Value = lastName },
                        new MySqlParameter() { ParameterName = "@i_chvICNumber", MySqlDbType = MySqlDbType.VarChar, Value = string.Empty },
                        new MySqlParameter() { ParameterName = "@i_chvPassportNo", MySqlDbType = MySqlDbType.VarChar, Value = string.Empty },
                        new MySqlParameter() { ParameterName = "@i_dtmDateOfBirth", MySqlDbType = MySqlDbType.DateTime, IsNullable = true, Value = new DateTime(1900, 1, 1).ToString("yyyy-MM-dd HH:mm:ss") },
                        new MySqlParameter() { ParameterName = "@i_intGender", MySqlDbType = MySqlDbType.Int32, Value = (int)Gender.MALE },
                        new MySqlParameter() { ParameterName = "@i_guidNationality", MySqlDbType = MySqlDbType.VarChar, IsNullable = true, Value = null },
                        new MySqlParameter() { ParameterName = "@i_chvTimeZone", MySqlDbType = MySqlDbType.VarChar, Value = Constant.TIME_ZONE_ID },
                        new MySqlParameter() { ParameterName = "@i_chvEmail", MySqlDbType = MySqlDbType.VarChar, Value = Data.Email },
                        new MySqlParameter() { ParameterName = "@i_chvPhoneNo", MySqlDbType = MySqlDbType.VarChar, Value = Data.PhoneNo },
                        new MySqlParameter() { ParameterName = "@i_guidImageId", MySqlDbType = MySqlDbType.VarChar, IsNullable = true, Value = null },
                        new MySqlParameter() { ParameterName = "@i_chvAddress", MySqlDbType = MySqlDbType.VarChar, IsNullable = true, Value = Data.Address },
                        new MySqlParameter() { ParameterName = "@i_chvCity", MySqlDbType = MySqlDbType.VarChar, Value = Data.City },
                        new MySqlParameter() { ParameterName = "@i_chvPostcode", MySqlDbType = MySqlDbType.VarChar, Value = Data.Postcode },
                        new MySqlParameter() { ParameterName = "@i_guidState", MySqlDbType = MySqlDbType.VarChar, IsNullable = true, Value = stateId },
                        new MySqlParameter() { ParameterName = "@i_chvState", MySqlDbType = MySqlDbType.VarChar, IsNullable = true, Value = state },
                        new MySqlParameter() { ParameterName = "@i_guidCountry", MySqlDbType = MySqlDbType.VarChar, Value = countryId },
                        new MySqlParameter() { ParameterName = "@i_intStatus", MySqlDbType = MySqlDbType.Int32, Value = status },
                        new MySqlParameter() { ParameterName = "@i_guidRefAgentId", MySqlDbType = MySqlDbType.VarChar, IsNullable = true, Value = Data.RefAgentId },
                        new MySqlParameter() { ParameterName = "@i_dtmProcessDate", MySqlDbType = MySqlDbType.DateTime, Value = curr.ToString("yyyy-MM-dd HH:mm:ss") }
                    };

                    var result = db.Database.SqlQuery<SP_add_user_registration_Result>(
                        @"call sp_user_registration (@i_guidRequestor, @i_chvUserName, @i_chvPassSalt, @i_chvPassword, @i_chvFirstName, @i_chvLastName, @i_chvICNumber, 
                            @i_chvPassportNo, @i_dtmDateOfBirth, @i_intGender, @i_guidNationality, @i_chvTimeZone, @i_chvEmail, @i_chvPhoneNo, @i_guidImageId,
                            @i_chvAddress, @i_chvCity, @i_chvPostcode, @i_guidState, @i_chvState, @i_guidCountry, @i_intStatus, 
                            @i_guidRefAgentId, @i_dtmProcessDate)", param);

                    var retObj = result.FirstOrDefault();

                    if (retObj != null)
                    {
                        retCode = retObj.CodeResult;
                        retMsg = retObj.MsgResult;

                        retVal = retObj.IsSucceed;

                        if (retObj.IsSucceed)
                        {
                            //assign output value
                            user_newId = retObj.UserId;
                        }
                    }
                }
                catch
                { }

                //assign output variable
                RespObj = new RespArgs<Guid?>
                {
                    Error = (!retVal),
                    Code = retCode,
                    Message = retMsg,
                    ObjVal = user_newId
                };
            }

            //implement this `callback` to separate the `Email sending` from `Account create` and speed-up the page respond for `user UIX`

            if (retVal)
            {
                Action<EmailCallbackEventArgs<CustomerRegistrationModel>> callback = CustomerRegistrationEmailCallback;

                Guid tmpNewUsrId = user_newId ?? Guid.Empty;

                Task<bool> task = CustomerRegistrationSendEmail(Data, tmpNewUsrId, callback);
            }

            return retVal;
        }

        private static async Task<bool> CustomerRegistrationSendEmail(CustomerRegistrationModel Data, Guid NewUserId, Action<EmailCallbackEventArgs<CustomerRegistrationModel>> Callback)
        {
            bool retVal = false;

            if (Callback != null)
            {
                EmailCallbackEventArgs<CustomerRegistrationModel> args = new EmailCallbackEventArgs<CustomerRegistrationModel>
                {
                    NewUserId = NewUserId,
                    Data = Data,
                };

                await Task.Delay(500).ContinueWith(c =>
                {
                    //trigger callback
                    Callback(args);
                }).ConfigureAwait(false);
            }

            return retVal;
        }

        private static void CustomerRegistrationEmailCallback(EmailCallbackEventArgs<CustomerRegistrationModel> obj)
        {
            if (obj == null || obj.Data == null || obj.NewUserId == Guid.Empty)
            { return; }

            try
            {
                setting emailTemplateNewMemberCreation = null;
                setting emailTemplateAdminNewMemberCreation = null;
                setting adminEmail = null;

                using (DataContext db = new DataContext())
                {
                    //get `setting`
                    IQueryable<setting> settingExist = db.settings;

                    emailTemplateNewMemberCreation = settingExist.Where(w => w.Ind == (int)ConfigSettingType.EMAIL_NEW_MEMB_CREATION).FirstOrDefault();
                    emailTemplateAdminNewMemberCreation = settingExist.Where(w => w.Ind == (int)ConfigSettingType.EMAIL_ADMIN_NEW_MEMB_CREATION).FirstOrDefault();
                    adminEmail = settingExist.Where(w => w.Ind == (int)ConfigSettingType.ADMINISTRATOR_EMAIL).FirstOrDefault();
                }

                if (emailTemplateNewMemberCreation != null)
                {
                    string emailSubject = emailTemplateNewMemberCreation.StrValue;
                    string emailContent = emailTemplateNewMemberCreation.TextValue;

                    emailContent = emailContent.Replace("{Email}", obj.Data.Email);
                    //emailContent = emailContent.Replace("[Password]", obj.Data.Password);
                    emailContent = emailContent.Replace("{Firstname}", obj.Data.FirstName);
                    emailContent = emailContent.Replace("{Lastname}", obj.Data.LastName);

                    bool emailBool = false;

                    System.Net.Mail.MailAddress[] to = new System.Net.Mail.MailAddress[] { new System.Net.Mail.MailAddress(obj.Data.Email) };
                    System.Net.Mail.MailAddress[] cc = new System.Net.Mail.MailAddress[] { };
                    System.Net.Mail.MailAddress[] bcc = new System.Net.Mail.MailAddress[] { };
                    string[] attachments = new string[] { };

                    //send email - user
                    emailBool = GeneralHelper.SendEmail(emailSubject, emailContent, to, cc, bcc, attachments);

                    if (!emailBool)
                    { }

                    //email log
                    bool retEmailLogBool = MiscLogic.EmailLogging(emailBool, emailSubject, emailContent, to, cc, bcc, attachments,
                        EmailType: EmailLogType.MEMB_NEW_CREATION, TargetUser: obj.NewUserId, Creator: obj.NewUserId);
                }

                if (emailTemplateAdminNewMemberCreation != null && adminEmail != null)
                {
                    string emailSubject = emailTemplateAdminNewMemberCreation.StrValue;
                    string emailContent = emailTemplateAdminNewMemberCreation.TextValue;

                    emailContent = emailContent.Replace("{Email}", obj.Data.Email);
                    //emailContent = emailContent.Replace("[Password]", obj.Data.Password);
                    emailContent = emailContent.Replace("{Firstname}", obj.Data.FirstName);
                    emailContent = emailContent.Replace("{Lastname}", obj.Data.LastName);

                    //admin1.mail.com;admin2.mai.com;admin3.mail.com
                    string[] emails = adminEmail.StrValue.Split(';');

                    List<System.Net.Mail.MailAddress> receiverList = new List<System.Net.Mail.MailAddress>();

                    foreach (var item in emails)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        { receiverList.Add(new System.Net.Mail.MailAddress(item.Trim())); }
                    }

                    bool emailBool = false;

                    System.Net.Mail.MailAddress[] to = new System.Net.Mail.MailAddress[] { };
                    System.Net.Mail.MailAddress[] cc = new System.Net.Mail.MailAddress[] { };
                    System.Net.Mail.MailAddress[] bcc = new System.Net.Mail.MailAddress[] { };
                    string[] attachments = new string[] { };

                    if (receiverList.Count > 0)
                    {
                        //convert to `Array`
                        to = receiverList.ToArray();

                        //send email - administrator
                        emailBool = GeneralHelper.SendEmail(emailSubject, emailContent, to, cc, bcc, attachments);

                        if (!emailBool)
                        { }

                        //email log
                        bool retEmailLogBool = MiscLogic.EmailLogging(emailBool, emailSubject, emailContent, to, cc, bcc, attachments,
                            EmailType: EmailLogType.ADMIN_NEW_MEMB_CREATION, TargetUser: obj.NewUserId, Creator: obj.NewUserId);
                    }
                }
            }
            catch
            { }
        }

        //Class
        #region Private Class
        private class EmailCallbackEventArgs<T> where T : new()
        {
            public EmailCallbackEventArgs()
            {
                this.Data = new T();
            }

            public T Data { get; set; }

            public Guid NewUserId { get; set; }
        }
        #endregion
    }
}
