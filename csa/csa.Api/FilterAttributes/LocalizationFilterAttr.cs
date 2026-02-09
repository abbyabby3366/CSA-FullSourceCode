using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace csa.Api.FilterAttributes
{
    public class LocalizationFilterAttr : ActionFilterAttribute
    {
        private string defaultLang = "en";
        private string[] allowLang = { "en", "cn", "zh", "ms" };

        public LocalizationFilterAttr()
        {

        }

        public override void OnActionExecuting(HttpActionContext ActionContext)
        {
            HttpContext ctx = HttpContext.Current;

            string lang = ctx.Request.QueryString["lang"];

            if (string.IsNullOrEmpty(lang))
            { lang = defaultLang; }

            if (!allowLang.Contains(lang))
            { lang = defaultLang; }

            try
            {
                CultureInfo culture = null;
                DateTimeFormatInfo dtf = null;

                switch (lang)
                {
                    case "en":
                        culture = new CultureInfo("en-US");

                        dtf = culture.DateTimeFormat;
                        dtf.Calendar = new GregorianCalendar();
                        dtf.DateSeparator = "/";
                        dtf.ShortDatePattern = "yyyy/MM/dd";
                        dtf.ShortTimePattern = "HH:mm:ss";

                        break;
                    case "cn":
                    case "zh":
                        culture = new CultureInfo("zh-CN");

                        dtf = culture.DateTimeFormat;
                        dtf.Calendar = new GregorianCalendar();
                        dtf.DateSeparator = "/";
                        dtf.ShortDatePattern = "yyyy/MM/dd";
                        dtf.ShortTimePattern = "HH:mm:ss";

                        break;
                    case "ms":
                        culture = new CultureInfo("ms-MY");

                        dtf = culture.DateTimeFormat;
                        dtf.Calendar = new GregorianCalendar();
                        dtf.DateSeparator = "/";
                        dtf.ShortDatePattern = "yyyy/MM/dd";
                        dtf.ShortTimePattern = "HH:mm:ss";

                        break;
                    default:
                        culture = new CultureInfo("en");

                        dtf = culture.DateTimeFormat;
                        dtf.Calendar = new GregorianCalendar();
                        dtf.DateSeparator = "/";
                        dtf.ShortDatePattern = "yyyy/MM/dd";
                        dtf.ShortTimePattern = "HH:mm:ss";

                        break;
                }

                Thread.CurrentThread.CurrentUICulture = culture;
                Thread.CurrentThread.CurrentCulture = culture;
            }
            catch
            { }
        }
    }
}