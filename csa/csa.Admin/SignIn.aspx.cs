using csa.Admin.Helpers;
using csa.Model.DataObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csa.Admin
{
    public partial class SignIn : System.Web.UI.Page
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
        }

        //Events
        #region Events
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ////validation
            //bool validationBool = true;

            //string modalTitle = "Missing required field(s)";
            //string modalMsg = string.Empty;

            //if (string.IsNullOrEmpty(txtEmail.Value) || !Utils.Validation.IsEmail(txtEmail.Value))
            //{ modalMsg = "Email required"; validationBool = false; }

            //if (string.IsNullOrEmpty(txtPassword.Value.Trim()))
            //{ modalMsg = "Password required"; validationBool = false; }

            //if (!validationBool)
            //{
            //    //set modal display content
            //    litLoginModalTitle.Text = modalTitle;
            //    litLoginModalMessage.Text = modalMsg;

            //    string script = "$(function() { showModal(); });";

            //    ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "showModal", script, true);
            //    //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "showModal", script, true);

            //    return;
            //}

            ////login verification
            //RespArgs<LoginAdminModel> respObj = null;

            //var retBool = LoginLogic.LoginAuthByAdmin(txtEmail.Value, txtPassword.Value, out respObj);

            //if (respObj == null || respObj.Error)
            //{
            //    //set modal display content
            //    litLoginModalTitle.Text = "Login Failed";
            //    litLoginModalMessage.Text = "Invalid login credential";

            //    string script = "$(function() { showModal(); });";

            //    ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "showModal", script, true);

            //    return;
            //}

            ////set `session`
            //SessionManager.AdminSession = respObj.ObjVal;

            SessionManager.SetLoginAdmin(JsonConvert.DeserializeObject<LoginAdmin>(hfSessionLogin.Value));
            //redirect
            Response.Redirect("~/Dashboard");
        }
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
