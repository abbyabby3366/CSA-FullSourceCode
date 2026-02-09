using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csa.Admin
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            if (!string.IsNullOrEmpty(Request["lang"]))
            {
                switch (Request.Params["lang"])
                {
                    case "en":
                        HttpContext.Current.Session["Language"] = "en";
                        break;
                    case "zh":
                    case "cn":
                        HttpContext.Current.Session["Language"] = "zh";
                        break;
                    case "ms":
                        HttpContext.Current.Session["Language"] = "ms";
                        break;

                    default:
                        break;
                }
            }

            string culture = (HttpContext.Current.Session["Language"] == null) ? "en" : HttpContext.Current.Session["Language"].ToString();

            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);

            base.InitializeCulture();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    bool isAllowAccessBool = false;
            //    bool isMaintenance = false;

            //    while (true)
            //    {
            //        //check is currently under maintenace
            //        isMaintenance = MaintenanceLogic.IsWebMaintenanceMode();

            //        if (isMaintenance)
            //        { break; }

            //        //set boolean
            //        isAllowAccessBool = true;
            //        break;
            //    }

            //    if (!isAllowAccessBool)
            //    {
            //        if (isMaintenance)
            //        { Response.Redirect("~/Maintenance"); }
            //    }
            //    else
            //    {
            //        //systemLogic - redirect to `dashboard` IF session has value

            //        if (SessionManager.AdminSession != null)
            //        { Response.Redirect("~/Dashboard"); }
            //    }
            //}
        }

        //Events
        #region Events

        #endregion

        //Functions
        #region Functions

        #endregion

        //Properties
        #region Properties

        #endregion

        //Utilities

        #region Utilities
        protected string GetLangText(string key)
        {
            var globalresourcestring = (String)GetGlobalResourceObject("Lang", key);
            return globalresourcestring;
        }
        #endregion
    }
}
