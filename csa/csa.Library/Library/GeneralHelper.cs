using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace csa.Library
{
    public class GeneralHelper
    {
        public static bool SendEmail(string EmailSubject, string EmailContent, MailAddress[] EmailTo, MailAddress[] CC, MailAddress[] BCC,
            string[] AttachmentPath, bool IsBodyHtml = true)
        {
            string errMessage;
            return SendEmail(EmailSubject, EmailContent, EmailTo, CC, BCC, AttachmentPath, out errMessage, IsBodyHtml);
        }

        public static bool SendEmail(string EmailSubject, string EmailContent, MailAddress[] EmailTo, MailAddress[] CC, MailAddress[] BCC,
            string[] AttachmentPath, out string ErrorMessage, bool IsBodyHtml = true)
        {
            bool retVal = false;
           
            try
            {
                bool isAllowSendEmail = false;
                bool hasVoidEmail = false;

                //verify is send email feature enabled
                string isAllowSendEmailStr = ConfigurationManager.AppSettings["IsAllowSendEmail"];

                //void email
                string voidEmail = ConfigurationManager.AppSettings.Get("VoidEmailDomain");

                if (!int.TryParse(isAllowSendEmailStr, out int isAllowSendEmailInt)) { isAllowSendEmail = false; }
                else { isAllowSendEmail = (isAllowSendEmailInt == 1 ? true : false); }

                SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
                mailMsg.Subject = EmailSubject;
                mailMsg.IsBodyHtml = IsBodyHtml;
                mailMsg.From = new MailAddress(smtpSection.From);

                //send to
                foreach (var item in EmailTo)
                {
                    if (!item.Address.Contains(voidEmail))
                    { mailMsg.To.Add(item); }
                    else { hasVoidEmail = true; }
                }

                //cc
                foreach (var item in CC)
                {
                    if (!item.Address.Contains(voidEmail))
                    { mailMsg.CC.Add(item); }
                    else { hasVoidEmail = true; }
                }

                //bcc
                foreach (var item in BCC)
                {
                    if (!item.Address.Contains(voidEmail))
                    { mailMsg.Bcc.Add(item); }
                    else { hasVoidEmail = true; }
                }

                //attachment
                foreach (var item in AttachmentPath)
                {
                    if (!File.Exists(item))
                    { continue; }

                    mailMsg.Attachments.Add(new Attachment(item));
                }

                mailMsg.Body = EmailContent;

                SmtpClient smtpClient = new SmtpClient();
                //smtpClient.Host = smtpSection.Network.Host;
                //smtpClient.Port = smtpSection.Network.Port;
                //smtpClient.EnableSsl = smtpSection.Network.EnableSsl;
                //smtpClient.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                //smtpClient.DeliveryMethod = smtpSection.DeliveryMethod;

                ////credential
                //smtpClient.Credentials = new System.Net.NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);

                try
                {
                    if (!isAllowSendEmail)
                    { retVal = true; }
                    else if (mailMsg.To.Count == 0 && hasVoidEmail)
                    { retVal = true; }
                    else
                    {
                        //send mail
                        smtpClient.Send(mailMsg);

                        retVal = true;
                    }
                }
                catch (Exception ex)
                {
                    retVal = false;
                    ErrorMessage = ex.Message;
                    return retVal;
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                ErrorMessage = ex.Message;
                return retVal;
            }
            ErrorMessage = "";

            return retVal;
        }
    }
}
