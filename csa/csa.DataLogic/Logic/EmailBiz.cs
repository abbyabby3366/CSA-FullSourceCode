using csa.Library;
using CsaModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace csa.DataLogic
{
    public abstract class BaseEmailData
    {
        protected BaseEmailData(string to, string dear)
        {
            To = to;
            Dear = dear;
        }

        public string To { get; set; }
        public string Dear { get; set; }
    }

    public class EmailNewAssignedData : BaseEmailData
    {
        public EmailNewAssignedData(string to, string name, DateTime createDate, string fileNumber, string pFCName, string receiveName) : base(to, name)
        {
            Name = name;
            CreateDate = createDate;
            FileNumber = fileNumber;
            PFCName = pFCName;
            ReceiveName = receiveName;
        }

        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string FileNumber { get; set; }
        public string PFCName { get; set; }
        public string ReceiveName { get; set; }
    }

    public class EmailNewClientRegistrationData
    {
        public EmailNewClientRegistrationData(string name, DateTime createDate, string phoneNumber, string fileNumber)
        {
            Name = name;
            CreateDate = createDate;
            PhoneNumber = phoneNumber;
            FileNumber = fileNumber;
        }

        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string PhoneNumber { get; set; }
        public string FileNumber { get; set; }
    }

    public class EmailNewData : BaseEmailData
    {
        public EmailNewData(string to, string dear, string password, string linkLogin, string username) : base(to, dear)
        {
            Password = password;
            LinkLogin = linkLogin;
            Username = username;
        }
        public string Password { get; set; }
        public string LinkLogin { get; set; }
        public string Username { get; set; }
    }

    public class EmailNewDirectData : BaseEmailData
    {
        public EmailNewDirectData(string to, string subject, string body) : base(to, "")
        {
            Subject = subject;
            Body = body;
        }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public static class EmailBiz
    {        
        public static void NewClientRegistration(CsaEntities db, EmailNewClientRegistrationData data)
        {
            var setting = SettingBiz.Get("NewClientRegistrationEmail");
            var settingAdminEmails = SettingBiz.Get("AdminEmails");
            string to = settingAdminEmails.StrValue;
            string subject = setting.StrValue;
            StringBuilder sb = new StringBuilder(setting.TextValue);
            sb.Replace("{ClientName}", data.Name);
            sb.Replace("{CreateDate}", data.CreateDate.ToString("dd MMM yyyy HH:mm"));
            sb.Replace("{PhoneNumber}", data.PhoneNumber);
            sb.Replace("{FileNumber}", data.FileNumber);
            sb.Replace("{Dear}", settingAdminEmails.TextValue);

            string body = sb.ToString();
            var newEmail = new Email 
            {
                Subject = subject,
                To = to,
                CreateDate = DateTime.Now,
                Body = body
            };
            db.Emails.AddObject(newEmail);
            db.SaveChanges();
        }

        public static void NewClientAssigned(CsaEntities db, EmailNewAssignedData data)
        {
            var setting = SettingBiz.Get("NewClientAssignedEmail");
            string subject = setting.StrValue;
            StringBuilder sb = new StringBuilder(setting.TextValue);
            sb.Replace("{ClientName}", data.Name);
            sb.Replace("{CreateDate}", data.CreateDate.ToString("dd MMM yyyy HH:mm"));
            sb.Replace("{PFCName}", data.PFCName);
            sb.Replace("{FileNumber}", data.FileNumber);
            sb.Replace("{Dear}", data.ReceiveName);

            string body = sb.ToString();
            var newEmail = new Email
            {
                Subject = subject,
                To = data.To,
                CreateDate = DateTime.Now,
                Body = body
            };
            db.Emails.AddObject(newEmail);
            db.SaveChanges();
        }

        public static void YabamCompleteSurvey(CsaEntities db, EmailNewData data)
        {
            var setting = SettingBiz.Get("YabamCompleteSurveyEmail");
            string subject = setting.StrValue;
            StringBuilder sb = new StringBuilder(setting.TextValue);
            sb.Replace("{Dear}", data.Dear);
            sb.Replace("{Link}", data.LinkLogin);
            sb.Replace("{Password}", data.Password);
            sb.Replace("{Username}", data.Username);

            string body = sb.ToString();
            var newEmail = new Email
            {
                Subject = subject,
                To = data.To,
                CreateDate = DateTime.Now,
                Body = body
            };
            db.Emails.AddObject(newEmail);
            db.SaveChanges();
        }

        public static void DirectEmail(CsaEntities db, EmailNewDirectData data)
        {
            var newEmail = new Email
            {
                Subject = data.Subject,
                To = data.To,
                CreateDate = DateTime.Now,
                Body = data.Body
            };
            db.Emails.AddObject(newEmail);
            db.SaveChanges();
        }

        public static void SendEmail(int take,out string errorMessage)
        {
            errorMessage = "";
            using (CsaEntities db = new CsaEntities())
            {
                int sendEmailPerTimeInt = take;

                string username = " ";
                string from = " ";
                string password = "";
                string host = "";
                int port = 0;
                bool enableSsl = false;
                try
                {
                    var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                    username = smtpSection.Network.UserName;
                    from = smtpSection.From;
                    password = smtpSection.Network.Password;
                    host = smtpSection.Network.Host;
                    port = smtpSection.Network.Port;
                    enableSsl = smtpSection.Network.EnableSsl;
                }
                catch (Exception)
                {

                }

                var emails = db.Emails.Where(x => x.SentDate == null).OrderByDescending(x => x.CreateDate).Take(sendEmailPerTimeInt);

                StringBuilder sb = new StringBuilder();

                foreach (var email in emails)
                {
                    System.Net.Mail.MailMessage myMessage = new System.Net.Mail.MailMessage();
                    myMessage.Subject = email.Subject;
                    myMessage.IsBodyHtml = true;
                    myMessage.Body = email.Body;

                    myMessage.From = new System.Net.Mail.MailAddress(username, from);
                    string[] to = email.To.Split(',');
                    foreach (var e in to)
                    {
                        myMessage.To.Add(new System.Net.Mail.MailAddress(e));
                    }

                    //cc
                    if (email.Cc != null && email.Cc != "")
                    {
                        foreach (string cc in email.Cc.Split(';'))
                        {
                            myMessage.CC.Add(cc.Trim());
                        }
                    }

                    //bcc
                    if (email.Bcc != null && email.Bcc != "")
                    {
                        foreach (string bcc in email.Bcc.Split(';'))
                        {
                            myMessage.Bcc.Add(bcc.Trim());
                        }
                    }

                    //attachment
                    if (email.AttachmentFileId != null && email.AttachmentFileId != "")
                    {
                        if (System.IO.File.Exists(email.AttachmentFileId))
                        {
                            System.Net.Mail.Attachment attachment;
                            attachment = new System.Net.Mail.Attachment(email.AttachmentFileId);
                            if (!email.AttachmentFilename.IsEmpty()) attachment.Name = email.AttachmentFilename;
                            myMessage.Attachments.Add(attachment);
                        }
                    }

                    System.Net.Mail.SmtpClient mySmtpClient = new System.Net.Mail.SmtpClient
                    {
                        // Use SMTP settings from configuration
                        Host = host, // SMTP host (e.g., smtp.example.com)
                        Port = port, // SMTP port (usually 587 or 465)
                        EnableSsl = enableSsl // Enable SSL if required by the SMTP server
                    };

                    mySmtpClient.Credentials = new System.Net.NetworkCredential(username, password);
                    try
                    {
                        mySmtpClient.Send(myMessage);
                        email.SentDate = DateTime.Now;
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
