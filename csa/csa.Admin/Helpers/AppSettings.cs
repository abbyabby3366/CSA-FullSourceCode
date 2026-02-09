using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace csa.Member.Helpers
{
    public static class AppSettings
    {
        public static string UploadPath => ConfigurationManager.AppSettings["UploadPath"];
        public static string MemberAppPath => ConfigurationManager.AppSettings["MemberAppPath"];
        //public static string UploadApplicatoinPreCheckingPath => UploadPath + "\\application\\pre_checking";
    }
}