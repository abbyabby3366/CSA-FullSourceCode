using System;

namespace csa.Library
{
    public class Constant
    {
        //default timeZone
        public const string TIME_ZONE_ID = "Singapore Standard Time";

        //encrypt key
        public const string CryptoKey = "Vc9M38h2";

        //system Id
        public static readonly Guid SYSTEM_ID = new Guid("782b90ae-a4a4-412e-8178-d734aebe16b4");

        //================================================================================================

        public static string URL_SRC_PATH = System.Configuration.ConfigurationManager.AppSettings.Get("UrlSrcPath");

        public static string BASE_FILE_PATH = System.IO.Path.Combine(URL_SRC_PATH, "uploads/");

        //------------------------------------------------------------------------------------------------

        //constant file path
        public const string USER_IMG = "profile/";
        public const string THUMBNAIL = "_thumbnail";

        //================================================================================================

        public class Area
        {
            public const int DASHBOARD = 1;

            public const int POLICY = 11;

            public const int LEADS_FORM_ENQUIRY = 51;

            public const int MEMBER = 21;

            public const int REPORTING = 31;

            public const int SETTINGS = 41;

            public const int GLOBAL_SETTINGS = 91;
        }

        public static int OTHER_NUMBER = 999;
    }
}