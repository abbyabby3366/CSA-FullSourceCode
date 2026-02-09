using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

using csa.Data;
using csa.Data.Cache;
using csa.Data.EntityModel;
using csa.Data.Library;
using csa.Library;
using csa.Model;

namespace csa.Data.Logic
{
    public class MiscLogic : BaseLogic
    {
        /// <summary>
        /// Email logging to `email_log` table
        /// </summary>
        /// <param name="IsEmailSent">Is email sent - 1: YES; 0: NO;</param>
        /// <param name="EmailSubject">Email subject</param>
        /// <param name="EmailContent">Email Content</param>
        /// <param name="EmailTo">Email receiver(s)</param>
        /// <param name="CC">Email CC receiver(s)</param>
        /// <param name="BCC">Email BCC receiver(s)</param>
        /// <param name="AttachmentPath">Email attachement ([Note]: File path)</param>
        /// <param name="IsBodyHtml"></param>
        /// <param name="EmailType">EmailLogType</param>
        /// <param name="TargetUser">Email log referrer</param>
        /// <param name="Creator">Email creator</param>
        /// <returns></returns>
        public static bool EmailLogging(bool IsEmailSent, string EmailSubject, string EmailContent, MailAddress[] EmailTo, MailAddress[] CC, MailAddress[] BCC,
            string[] AttachmentPath, bool IsBodyHtml = true, EmailLogType EmailType = EmailLogType.NONE, Guid? TargetUser = null, Guid? Creator = null)
        {
            bool retVal = false;

            try
            {
                DateTime curr = DateTime.UtcNow;
                Guid emailLog_newId = Guid.NewGuid();

                Guid createdBy = (Creator.HasValue) ? Creator.Value : Constant.SYSTEM_ID;

                //status
                EmailLogStatus emailLogSts = (IsEmailSent) ? EmailLogStatus.SENT : EmailLogStatus.PENDING;

                using (DataContext db = new DataContext())
                {
                    email_log emailLog_new = new email_log
                    {
                        Id = emailLog_newId,
                        Type = (int)EmailType,
                        UserId = TargetUser,
                        To = String.Join(";", EmailTo.Select(s => s.Address)),
                        CC = String.Join(";", CC.Select(s => s.Address)),
                        BCC = String.Join(";", BCC.Select(s => s.Address)),
                        MailSubject = EmailSubject,
                        MailContent = EmailContent,
                        Attachment = String.Join(";", AttachmentPath),
                        Status = (int)emailLogSts,
                        CreatedDate = curr,
                        CreatedBy = createdBy,
                        UpdatedDate = curr,
                        UpdatedBy = createdBy
                    };

                    db.email_log.Add(emailLog_new);

                    db.SaveChanges();
                }

                retVal = true;
            }
            catch
            { }

            return retVal;
        }

        //------------------------------------------------------------------------------------------------

        /// <summary>
        /// Send email by customer
        /// </summary>
        /// <param name="USid">User sessionId</param>
        /// <param name="EmailSubject">Email subject</param>
        /// <param name="EmailContent">Email body content</param>
        /// <param name="RespObj">Respond argument - [Return] New `UserId` IF email sent ELSE `Null`</param>
        /// <returns>[Return] Email send status boolean status</returns>
        public static bool SendEmailByCustomer(Guid? USid, string EmailSubject, string EmailContent, out RespArgs<Guid?> RespObj)
        {
            bool retVal = false;

            //default output variable
            RespObj = new RespArgs<Guid?> { Error = true };

            if (USid == null)
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.SESSION_TIMEOUT;
                RespObj.Message = "Session timeout";

                return retVal;
            }

            CustomerSessionCacheModel session = SessionCache.GetSessionCacheByKey<CustomerSessionCacheModel>(USid.Value);

            if (session == null)
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.SESSION_TIMEOUT;
                RespObj.Message = "Session timeout";

                return false;
            }

            Guid? userId_exist = null;

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `setting`
                    user userExist = db.users
                        .Where(w => w.Id == session.UserId)
                        .FirstOrDefault();

                    if (userExist == null)
                    {
                        RespObj.Error = true;
                        RespObj.Code = (int)ErrorCode.DATA_NOT_FOUND;
                        RespObj.Message = "Invalid user id";

                        return false;
                    }

                    //validation

                    if (string.IsNullOrWhiteSpace(userExist.Email))
                    {
                        RespObj.Error = true;
                        RespObj.Code = (int)ErrorCode.INVALID_INPUT;
                        RespObj.Message = "Invalid email address";

                        return false;
                    }

                    //assign local variable
                    userId_exist = userExist.Id;

                    string emailSubject = EmailSubject;
                    string emailContent = EmailContent;

                    bool emailBool = false;

                    System.Net.Mail.MailAddress[] to = new System.Net.Mail.MailAddress[] { new System.Net.Mail.MailAddress(userExist.Email) };
                    System.Net.Mail.MailAddress[] cc = new System.Net.Mail.MailAddress[] { };
                    System.Net.Mail.MailAddress[] bcc = new System.Net.Mail.MailAddress[] { };
                    string[] attachments = new string[] { };

                    //send email - user
                    emailBool = GeneralHelper.SendEmail(emailSubject, emailContent, to, cc, bcc, attachments);

                    if (emailBool)
                    { retVal = true; }

                    //email log
                    bool retEmailLogBool = MiscLogic.EmailLogging(emailBool, emailSubject, emailContent, to, cc, bcc, attachments,
                        EmailType: EmailLogType.NONE, TargetUser: userExist.Id, Creator: userExist.Id);
                }
            }
            catch
            { }

            if (retVal)
            {
                RespObj = new RespArgs<Guid?>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = userId_exist
                };
            }
            else
            {
                RespObj = new RespArgs<Guid?>
                {
                    Error = true,
                    Code = (int)ErrorCode.FAILED,
                    Message = "Failed to send email"
                };
            }

            return retVal;
        }

        //================================================================================================

        /// <summary>
        /// Upload file and Add record to `data_file` by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <param name="PostedFile">HttpPostedFile file</param>
        /// <param name="RelativeChildFilePath">All upload files will fall under "UploadPath" file path, which `RelativeChildFilePath` will be the child path</param>
        /// <param name="RespObj">Respond argument - [Return] New `FileDataId` IF file uploaded ELSE `Null`</param>
        /// <returns>[Return] Add/Upload `file_data` boolean status</returns>
        public static bool UploadFileByAdmin(Guid? ASid, HttpPostedFile PostedFile, string RelativeChildFilePath, out RespArgs<Guid?> RespObj)
        {
            bool retVal = false;

            //default output variable
            RespObj = new RespArgs<Guid?> { Error = true };

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

            Guid? fileData_newId = null;
            DateTime curr = DateTime.UtcNow;

            try
            {
                //get upload path
                string uploadPath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadPath");

                if (string.IsNullOrWhiteSpace(uploadPath))
                {
                    RespObj.Error = true;
                    RespObj.Code = (int)ErrorCode.FAILED;
                    RespObj.Message = "Upload path is not setup";

                    return false;
                }

                string serverPath = string.Empty;

                if (string.IsNullOrWhiteSpace(RelativeChildFilePath))
                { serverPath = System.IO.Path.Combine(uploadPath); }
                else
                { serverPath = System.IO.Path.Combine(uploadPath, RelativeChildFilePath); }

                //check upload directory
                if (!System.IO.Directory.Exists(serverPath))
                { System.IO.Directory.CreateDirectory(serverPath); }

                //assign local variable
                fileData_newId = Guid.NewGuid();

                //HttpPostedFileBase fub = (HttpPostedFileBase)item;

                //byte[] buff = new byte[item.ContentLength];

                //item.InputStream.Read(buff, 0, item.ContentLength);

                //using (var fs = new System.IO.FileStream(serverPath, System.IO.FileMode.Create))
                //{
                //    using (var br = new System.IO.BinaryWriter(fs))
                //    {
                //        br.Write(buff);
                //    }
                //}

                System.IO.FileInfo file = new System.IO.FileInfo(PostedFile.FileName);

                double fileSize = PostedFile.ContentLength;

                if (fileSize == 0)
                {
                    RespObj.Error = true;
                    RespObj.Code = (int)ErrorCode.REQUIRED_PARAMETER_NOT_SUPPLIED;
                    RespObj.Message = "No upload file";

                    return false;
                }

                using (var fs = System.IO.File.Create(System.IO.Path.Combine(serverPath, $"{fileData_newId}{file.Extension}")))
                {
                    PostedFile.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                    PostedFile.InputStream.CopyTo(fs);
                }

                try
                {
                    using (DataContext db = new DataContext())
                    {
                        file_data fileData_new = new file_data
                        {
                            Id = fileData_newId.Value,
                            Size = fileSize,
                            Filename = file.Name,
                            Extension = file.Extension,
                            //Description = null,
                            CreatedDate = curr
                        };

                        //add `file_data`
                        db.file_data.Add(fileData_new);
                        db.SaveChanges();
                    }

                    retVal = true;
                }
                catch
                { }
            }
            catch
            { }

            if (retVal)
            {
                //assign output variable
                RespObj = new RespArgs<Guid?>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = fileData_newId
                };
            }
            else
            {
                //assign output variable
                RespObj = new RespArgs<Guid?>
                {
                    Error = true,
                    Code = (int)ErrorCode.FAILED,
                    Message = "Upload file failed"
                };
            }

            return retVal;
        }

        /// <summary>
        /// Delete file and `data_file` record by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <param name="FileDataId">FileData id</param>
        /// <param name="RelativeChildFilePath">All uploaded files will fall under "UploadPath" file path, which `RelativeChildFilePath` will be the indicate child path for the deleting file located</param>
        /// <param name="RespObj">Respond argument - [Return] Deleted `FileDataId` IF file has deleted ELSE `Null`</param>
        /// <returns>[Return] Delete `file_data` boolean status</returns>
        public static bool DeleteFileByAdmin(Guid? ASid, Guid FileDataId, string RelativeChildFilePath, out RespArgs<Guid?> RespObj)
        {
            bool retVal = false;

            //default output variable
            RespObj = new RespArgs<Guid?> { Error = true };

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

            Guid? fileDataId_exist = null;
            DateTime curr = DateTime.UtcNow;

            //get upload path
            string uploadPath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadPath");

            if (string.IsNullOrWhiteSpace(uploadPath))
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.FAILED;
                RespObj.Message = "Upload path is not setup";

                return false;
            }

            string serverPath = string.Empty;
            string physicalPath = string.Empty;

            if (string.IsNullOrWhiteSpace(RelativeChildFilePath))
            { serverPath = System.IO.Path.Combine(uploadPath); }
            else
            { serverPath = System.IO.Path.Combine(uploadPath, RelativeChildFilePath); }

            try
            {
                using (DataContext db = new DataContext())
                {
                    file_data fileDataExist = db.file_data.Where(w => w.Id == FileDataId).FirstOrDefault();

                    if (fileDataExist == null)
                    {
                        RespObj.Error = true;
                        RespObj.Code = (int)ErrorCode.INVALID_INPUT;
                        RespObj.Message = "Invalid id";

                        return false;
                    }

                    //assign local variable
                    fileDataId_exist = fileDataExist.Id;

                    //set `physicalPath`
                    physicalPath = System.IO.Path.Combine(serverPath, $"{fileDataExist.Id}{fileDataExist.Extension}");

                    //delete `file_data`
                    db.file_data.Remove(fileDataExist);
                    db.SaveChanges();
                }

                retVal = true;
            }
            catch
            { }

            if (retVal)
            {
                try
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(physicalPath);

                    if (fi.Exists)
                    { fi.Delete(); }
                }
                catch { }

                //assign output variable
                RespObj = new RespArgs<Guid?>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = fileDataId_exist
                };
            }
            else
            {
                //assign output variable
                RespObj = new RespArgs<Guid?>
                {
                    Error = true,
                    Code = (int)ErrorCode.FAILED,
                    Message = "Delete file failed"
                };
            }

            return retVal;
        }

        //------------------------------------------------------------------------------------------------

        /// <summary>
        /// Upload file and Add record to `data_file` by customer
        /// </summary>
        /// <param name="USid">User sessionId</param>
        /// <param name="PostedFile">HttpPostedFile file</param>
        /// <param name="RelativeChildFilePath">All upload files will fall under "UploadPath" file path, which `RelativeChildFilePath` will be the child path</param>
        /// <param name="RespObj">Respond argument - [Return] New `FileDataId` IF file uploaded ELSE `Null`</param>
        /// <returns>[Return] Add/Upload `file_data` boolean status</returns>
        public static bool UploadFileByUser(Guid? USid, HttpPostedFile PostedFile, string RelativeChildFilePath, out RespArgs<Guid?> RespObj)
        {
            bool retVal = false;

            //default output variable
            RespObj = new RespArgs<Guid?> { Error = true };

            if (USid == null)
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.SESSION_TIMEOUT;
                RespObj.Message = "Session timeout";

                return retVal;
            }

            CustomerSessionCacheModel session = SessionCache.GetSessionCacheByKey<CustomerSessionCacheModel>(USid.Value);

            if (session == null)
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.SESSION_TIMEOUT;
                RespObj.Message = "Session timeout";

                return false;
            }

            Guid? fileData_newId = null;
            DateTime curr = DateTime.UtcNow;

            try
            {
                //get upload path
                string uploadPath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadPath");

                if (string.IsNullOrWhiteSpace(uploadPath))
                {
                    RespObj.Error = true;
                    RespObj.Code = (int)ErrorCode.FAILED;
                    RespObj.Message = "Upload path is not setup";

                    return false;
                }

                string serverPath = string.Empty;

                if (string.IsNullOrWhiteSpace(RelativeChildFilePath))
                { serverPath = System.IO.Path.Combine(uploadPath); }
                else
                { serverPath = System.IO.Path.Combine(uploadPath, RelativeChildFilePath); }

                //check upload directory
                if (!System.IO.Directory.Exists(serverPath))
                { System.IO.Directory.CreateDirectory(serverPath); }

                //assign local variable
                fileData_newId = Guid.NewGuid();

                //HttpPostedFileBase fub = (HttpPostedFileBase)item;

                //byte[] buff = new byte[item.ContentLength];

                //item.InputStream.Read(buff, 0, item.ContentLength);

                //using (var fs = new System.IO.FileStream(serverPath, System.IO.FileMode.Create))
                //{
                //    using (var br = new System.IO.BinaryWriter(fs))
                //    {
                //        br.Write(buff);
                //    }
                //}

                System.IO.FileInfo file = new System.IO.FileInfo(PostedFile.FileName);

                double fileSize = PostedFile.ContentLength;

                if (fileSize == 0)
                {
                    RespObj.Error = true;
                    RespObj.Code = (int)ErrorCode.REQUIRED_PARAMETER_NOT_SUPPLIED;
                    RespObj.Message = "No upload file";

                    return false;
                }

                using (var fs = System.IO.File.Create(System.IO.Path.Combine(serverPath, $"{fileData_newId}{file.Extension}")))
                {
                    PostedFile.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                    PostedFile.InputStream.CopyTo(fs);
                }

                try
                {
                    using (DataContext db = new DataContext())
                    {
                        file_data fileData_new = new file_data
                        {
                            Id = fileData_newId.Value,
                            Size = fileSize,
                            Filename = file.Name,
                            Extension = file.Extension,
                            //Description = null,
                            CreatedDate = curr
                        };

                        //add `file_data`
                        db.file_data.Add(fileData_new);
                        db.SaveChanges();
                    }

                    retVal = true;
                }
                catch
                { }
            }
            catch
            { }

            if (retVal)
            {
                //assign output variable
                RespObj = new RespArgs<Guid?>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = fileData_newId
                };
            }
            else
            {
                //assign output variable
                RespObj = new RespArgs<Guid?>
                {
                    Error = true,
                    Code = (int)ErrorCode.FAILED,
                    Message = "Upload file failed"
                };
            }

            return retVal;
        }

        /// <summary>
        /// Delete file and `data_file` record by customer
        /// </summary>
        /// <param name="USid">User sessionId</param>
        /// <param name="FileDataId">FileData id</param>
        /// <param name="RelativeChildFilePath">All uploaded files will fall under "UploadPath" file path, which `RelativeChildFilePath` will be the indicate child path for the deleting file located</param>
        /// <param name="RespObj">Respond argument - [Return] Deleted `FileDataId` IF file has deleted ELSE `Null`</param>
        /// <returns>[Return] Delete `file_data` boolean status</returns>
        public static bool DeleteFileByUser(Guid? USid, Guid FileDataId, string RelativeChildFilePath, out RespArgs<Guid?> RespObj)
        {
            bool retVal = false;

            //default output variable
            RespObj = new RespArgs<Guid?> { Error = true };

            if (USid == null)
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.SESSION_TIMEOUT;
                RespObj.Message = "Session timeout";

                return retVal;
            }

            CustomerSessionCacheModel session = SessionCache.GetSessionCacheByKey<CustomerSessionCacheModel>(USid.Value);

            if (session == null)
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.SESSION_TIMEOUT;
                RespObj.Message = "Session timeout";

                return false;
            }

            Guid? fileDataId_exist = null;
            DateTime curr = DateTime.UtcNow;

            //get upload path
            string uploadPath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadPath");

            if (string.IsNullOrWhiteSpace(uploadPath))
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.FAILED;
                RespObj.Message = "Upload path is not setup";

                return false;
            }

            string serverPath = string.Empty;
            string physicalPath = string.Empty;

            if (string.IsNullOrWhiteSpace(RelativeChildFilePath))
            { serverPath = System.IO.Path.Combine(uploadPath); }
            else
            { serverPath = System.IO.Path.Combine(uploadPath, RelativeChildFilePath); }

            try
            {
                using (DataContext db = new DataContext())
                {
                    file_data fileDataExist = db.file_data.Where(w => w.Id == FileDataId).FirstOrDefault();

                    if (fileDataExist == null)
                    {
                        RespObj.Error = true;
                        RespObj.Code = (int)ErrorCode.INVALID_INPUT;
                        RespObj.Message = "Invalid id";

                        return false;
                    }

                    //assign local variable
                    fileDataId_exist = fileDataExist.Id;

                    //set `physicalPath`
                    physicalPath = System.IO.Path.Combine(serverPath, $"{fileDataExist.Id}{fileDataExist.Extension}");

                    //delete `file_data`
                    db.file_data.Remove(fileDataExist);
                    db.SaveChanges();
                }

                retVal = true;
            }
            catch
            { }

            if (retVal)
            {
                try
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(physicalPath);

                    if (fi.Exists)
                    { fi.Delete(); }
                }
                catch { }

                //assign output variable
                RespObj = new RespArgs<Guid?>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = fileDataId_exist
                };
            }
            else
            {
                //assign output variable
                RespObj = new RespArgs<Guid?>
                {
                    Error = true,
                    Code = (int)ErrorCode.FAILED,
                    Message = "Delete file failed"
                };
            }

            return retVal;
        }

        //------------------------------------------------------------------------------------------------

        /// <summary>
        /// Upload file and Add record to `data_file` by anonymous
        /// </summary>
        /// <param name="PostedFile">HttpPostedFile file</param>
        /// <param name="RelativeChildFilePath">All upload files will fall under "UploadPath" file path, which `RelativeChildFilePath` will be the child path</param>
        /// <param name="RespObj">Respond argument - [Return] New `FileDataId` IF file uploaded ELSE `Null`</param>
        /// <returns>[Return] Add/Upload `file_data` boolean status</returns>
        public static bool UploadFileByAnonymous(HttpPostedFile PostedFile, string RelativeChildFilePath, out RespArgs<Guid?> RespObj)
        {
            bool retVal = false;

            //default output variable
            RespObj = new RespArgs<Guid?> { Error = true };

            //biz logic :- anonymous can call this function

            Guid? fileData_newId = null;
            DateTime curr = DateTime.UtcNow;

            try
            {
                //get upload path
                string uploadPath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadPath");

                if (string.IsNullOrWhiteSpace(uploadPath))
                {
                    RespObj.Error = true;
                    RespObj.Code = -101;
                    RespObj.Message = "Upload path is not setup";

                    return false;
                }

                string serverPath = string.Empty;

                if (string.IsNullOrWhiteSpace(RelativeChildFilePath))
                { serverPath = System.IO.Path.Combine(uploadPath); }
                else
                { serverPath = System.IO.Path.Combine(uploadPath, RelativeChildFilePath); }

                //check upload directory
                if (!System.IO.Directory.Exists(serverPath))
                { System.IO.Directory.CreateDirectory(serverPath); }

                //assign local variable
                fileData_newId = Guid.NewGuid();

                System.IO.FileInfo file = new System.IO.FileInfo(PostedFile.FileName);

                double fileSize = PostedFile.ContentLength;

                if (fileSize == 0)
                {
                    RespObj.Error = true;
                    RespObj.Code = (int)ErrorCode.REQUIRED_PARAMETER_NOT_SUPPLIED;
                    RespObj.Message = "No upload file";

                    return false;
                }

                using (var fs = System.IO.File.Create(System.IO.Path.Combine(serverPath, $"{fileData_newId}{file.Extension}")))
                {
                    PostedFile.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                    PostedFile.InputStream.CopyTo(fs);
                }

                try
                {
                    using (DataContext db = new DataContext())
                    {
                        file_data fileData_new = new file_data
                        {
                            Id = fileData_newId.Value,
                            Size = fileSize,
                            Filename = file.Name,
                            Extension = file.Extension,
                            CreatedDate = curr
                        };

                        //add `file_data`
                        db.file_data.Add(fileData_new);
                        db.SaveChanges();
                    }

                    retVal = true;
                }
                catch
                { }
            }
            catch
            { }

            if (retVal)
            {
                //assign output variable
                RespObj = new RespArgs<Guid?>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = fileData_newId
                };
            }
            else
            {
                //assign output variable
                RespObj = new RespArgs<Guid?>
                {
                    Error = true,
                    Code = (int)ErrorCode.FAILED,
                    Message = "Upload file failed"
                };
            }

            return retVal;
        }

        /// <summary>
        /// Delete file and `data_file` record by anonymous
        /// </summary>
        /// <param name="FileDataId">FileData id</param>
        /// <param name="RelativeChildFilePath">All uploaded files will fall under "UploadPath" file path, which `RelativeChildFilePath` will be the indicate child path for the deleting file located</param>
        /// <param name="RespObj">Respond argument - [Return] Deleted `FileDataId` IF file has deleted ELSE `Null`</param>
        /// <returns>[Return] Delete `file_data` boolean status</returns>
        public static bool DeleteFileByAnonymous(Guid FileDataId, string RelativeChildFilePath, out RespArgs<Guid?> RespObj)
        {
            bool retVal = false;

            //default output variable
            RespObj = new RespArgs<Guid?> { Error = true };

            //biz logic :- anonymous can call this function

            Guid? fileDataId_exist = null;
            DateTime curr = DateTime.UtcNow;

            //get upload path
            string uploadPath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadPath");

            if (string.IsNullOrWhiteSpace(uploadPath))
            {
                RespObj.Error = true;
                RespObj.Code = (int)ErrorCode.FAILED;
                RespObj.Message = "Upload path is not setup";

                return false;
            }

            string serverPath = string.Empty;
            string physicalPath = string.Empty;

            if (string.IsNullOrWhiteSpace(RelativeChildFilePath))
            { serverPath = System.IO.Path.Combine(uploadPath); }
            else
            { serverPath = System.IO.Path.Combine(uploadPath, RelativeChildFilePath); }

            try
            {
                using (DataContext db = new DataContext())
                {
                    file_data fileDataExist = db.file_data.Where(w => w.Id == FileDataId).FirstOrDefault();

                    if (fileDataExist == null)
                    {
                        RespObj.Error = true;
                        RespObj.Code = (int)ErrorCode.INVALID_INPUT;
                        RespObj.Message = "Invalid id";

                        return false;
                    }

                    //assign local variable
                    fileDataId_exist = fileDataExist.Id;

                    //set `physicalPath`
                    physicalPath = System.IO.Path.Combine(serverPath, $"{fileDataExist.Id}{fileDataExist.Extension}");

                    //delete `file_data`
                    db.file_data.Remove(fileDataExist);
                    db.SaveChanges();
                }

                retVal = true;
            }
            catch
            { }

            if (retVal)
            {
                try
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(physicalPath);

                    if (fi.Exists)
                    { fi.Delete(); }
                }
                catch { }

                //assign output variable
                RespObj = new RespArgs<Guid?>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = fileDataId_exist
                };
            }
            else
            {
                //assign output variable
                RespObj = new RespArgs<Guid?>
                {
                    Error = true,
                    Code = (int)ErrorCode.FAILED,
                    Message = "Delete file failed"
                };
            }

            return retVal;
        }

        //================================================================================================

        /// <summary>
        /// Get `country` gridview listing by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <param name="PageIdx">Page index</param>
        /// <param name="PageSize">Page size</param>
        /// <param name="SortExpression">Sort expression</param>
        /// <param name="SortDirection">Sort direction</param>
        /// <param name="Predicate">Filtering - predicate</param>
        /// <returns>Respond argument - [Return] GridViewModel&lt;CountryGVModel&gt;</returns>
        public static RespArgs<GridViewModel<CountryGVModel>> GetCountryGVByAdmin(Guid? ASid,
            int PageIdx = 1, int PageSize = 10,
            string SortExpression = "", int SortDirection = (int)SortDirection.Ascending,
            Expression<Func<country, bool>> Predicate = null)
        {
            RespArgs<GridViewModel<CountryGVModel>> retVal = new RespArgs<GridViewModel<CountryGVModel>> { Error = true };

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

            //validation

            if (PageIdx < 1) { PageIdx = 1; }
            if (PageSize < 10) { PageSize = 10; }
            if (PageSize > 500) { PageSize = 500; }

            int[] allowSts = { (int)GlobalStatus.ACTIVE, (int)GlobalStatus.INACTIVE };

            IEnumerable<CountryGVModel> list = null;
            int totalItems = 0;
            DateTime curr = DateTime.UtcNow;

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `country` list
                    IQueryable<country> countries = db.countries
                        .Where(w => allowSts.Contains(w.Status));

                    if (Predicate != null)
                    { countries = countries.Where(Predicate); }

                    switch (SortExpression)
                    {
                        case "Name":
                            countries = countries.AddOrdering(o => o.Name, (SortDirection)SortDirection);
                            break;

                        case "OrderSeq":
                            countries = countries.AddOrdering(o => o.OrderSeq, (SortDirection)SortDirection);
                            break;

                        case "Status":
                            countries = countries.AddOrdering(o => o.Status, (SortDirection)SortDirection);
                            break;

                        default:
                            countries = countries.AddOrdering(o => o.OrderSeq, (SortDirection)SortDirection);
                            break;
                    }

                    //get `TotalItems`
                    totalItems = countries.Count();

                    //paging
                    countries = countries.Skip((PageIdx - 1) * PageSize).Take(PageSize);

                    //tolist
                    list = countries.Select(s => new CountryGVModel
                        {
                            DT_RowId = s.Id.ToString(),
                            Name = s.Name,
                            OrderSeq = s.OrderSeq,
                            Status = s.Status
                        })
                        .ToList();
                }

                //assign return object
                retVal = new RespArgs<GridViewModel<CountryGVModel>>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = new GridViewModel<CountryGVModel>
                    {
                        data = list,
                        recordsTotal = totalItems,
                        recordsFiltered = totalItems,
                        PageIndex = PageIdx,
                        PageSize = PageSize,
                        SortExpression = SortExpression,
                        SortDirection = SortDirection
                    }
                };
            }
            catch
            { }

            return retVal;
        }

        //------------------------------------------------------------------------------------------------

        /// <summary>
        /// Get `country` drop-down-list by anonymous
        /// </summary>
        /// <returns>Respond argument - [Return] IEnumerable&lt;DropDownListMode&lt;Guid&gt;&gt;</returns>
        public static RespArgs<IEnumerable<DropDownListModel<Guid>>> GetCountryDDL()
        {
            RespArgs<IEnumerable<DropDownListModel<Guid>>> retVal = new RespArgs<IEnumerable<DropDownListModel<Guid>>> { Error = true };

            //biz logic :- anonymous can call this function

            try
            {
                IEnumerable<DropDownListModel<Guid>> list = new List<DropDownListModel<Guid>>();

                using (DataContext db = new DataContext())
                {
                    //get `country` list
                    IQueryable<country> countries = db.countries
                        .Where(w => w.Status == (int)GlobalStatus.ACTIVE);


                    //tolist
                    list = countries
                        .OrderBy(o => o.OrderSeq)
                        .Select(s => new DropDownListModel<Guid>
                        {
                            Id = s.Id,
                            Name = s.Name
                        })
                        .ToList();
                }

                //construct output object
                retVal = new RespArgs<IEnumerable<DropDownListModel<Guid>>>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = list
                };
            }
            catch
            { }

            return retVal;
        }

        /// <summary>
        /// Get `state` gridview listing by admin
        /// </summary>
        /// <param name="ASid">Admin sessionId</param>
        /// <param name="PageIdx">Page index</param>
        /// <param name="PageSize">Page size</param>
        /// <param name="SortExpression">Sort expression</param>
        /// <param name="SortDirection">Sort direction</param>
        /// <param name="Predicate">Filtering - predicate</param>
        /// <returns>Respond argument - [Return] GridViewModel&lt;CountryGVModel&gt;</returns>
        public static RespArgs<GridViewModel<StateGVModel>> GetStateGVByAdmin(Guid? ASid, 
            int PageIdx = 1, int PageSize = 10,
            string SortExpression = "", int SortDirection = (int)SortDirection.Ascending,
            Expression<Func<state, bool>> Predicate = null)
        {
            RespArgs<GridViewModel<StateGVModel>> retVal = new RespArgs<GridViewModel<StateGVModel>> { Error = true };

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

            //validation

            if (PageIdx < 1) { PageIdx = 1; }
            if (PageSize < 10) { PageSize = 10; }
            if (PageSize > 500) { PageSize = 500; }

            int[] allowSts = { (int)GlobalStatus.ACTIVE, (int)GlobalStatus.INACTIVE };

            IEnumerable<StateGVModel> list = null;
            int totalItems = 0;
            DateTime curr = DateTime.UtcNow;

            try
            {
                using (DataContext db = new DataContext())
                {
                    //get `state` list
                    IQueryable<state> states = db.states;

                    if (Predicate != null)
                    { states = states.Where(Predicate); }

                    switch (SortExpression)
                    {
                        case "Name":
                            states = states.AddOrdering(o => o.Name, (SortDirection)SortDirection);
                            break;

                        case "OrderSeq":
                            states = states.AddOrdering(o => o.OrderSeq, (SortDirection)SortDirection);
                            break;

                        default:
                            states = states.AddOrdering(o => o.OrderSeq, (SortDirection)SortDirection);
                            break;
                    }

                    //get `TotalItems`
                    totalItems = states.Count();

                    //paging
                    states = states.Skip((PageIdx - 1) * PageSize).Take(PageSize);

                    //tolist
                    list = states.Select(s => new StateGVModel
                        {
                            DT_RowId = $"{s.Id}",
                            Name = s.Name,
                            CountryId = s.CountryId,
                            CountryName = (s.CountryData == null) ? string.Empty : s.CountryData.Name,
                            OrderSeq = s.OrderSeq
                        })
                        .ToList();
                }

                //assign return object
                retVal = new RespArgs<GridViewModel<StateGVModel>>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = new GridViewModel<StateGVModel>
                    {
                        data = list,
                        recordsTotal = totalItems,
                        recordsFiltered = totalItems,
                        PageIndex = PageIdx,
                        PageSize = PageSize,
                        SortExpression = SortExpression,
                        SortDirection = SortDirection
                    }
                };
            }
            catch
            { }

            return retVal;
        }

        //------------------------------------------------------------------------------------------------

        /// <summary>
        /// Get `state` drop-down-list by anonymous
        /// </summary>
        /// <param name="CountryId">[optional] :- Return specified `country` 's states IF pass in `CountryId`. By default `CountryId` is null</param>
        /// <returns>Respond argument - [Return] IEnumerable&lt;DropDownListMode&lt;Guid&gt;&gt;</returns>
        public static RespArgs<IEnumerable<DropDownListModel<Guid>>> GetStateDLL(Guid? CountryId = null)
        {
            RespArgs<IEnumerable<DropDownListModel<Guid>>> retVal = new RespArgs<IEnumerable<DropDownListModel<Guid>>> { Error = true };

            //biz logic :- anonymous can call this function

            try
            {
                IEnumerable<DropDownListModel<Guid>> list = new List<DropDownListModel<Guid>>();

                using (DataContext db = new DataContext())
                {
                    //get `state` list
                    IQueryable<state> states = db.states;

                    if (CountryId != null)
                    { states = states.Where(w => w.CountryId == CountryId); }

                    //tolist
                    list = states
                        .OrderBy(o => o.OrderSeq)
                        .Select(s => new DropDownListModel<Guid>
                        {
                            Id = s.Id,
                            Name = s.Name
                        })
                        .ToList();
                }

                //construct output object
                retVal = new RespArgs<IEnumerable<DropDownListModel<Guid>>>
                {
                    Error = false,
                    Code = (int)ErrorCode.OK,
                    Message = "Successful",
                    ObjVal = list
                };
            }
            catch
            { }

            return retVal;
        }
    }
}
