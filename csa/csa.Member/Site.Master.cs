using csa.Member.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csa.Member
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (SessionManager.CurrentLoginMember == null)
                Response.Redirect("signin");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //bool isAllowAccessBool = false;
                //bool isMaintenance = false;

                //LoginAdminModel session = null;
                //AdminSessionCacheModel cached = null;

                //while (true)
                //{
                //    //check is currently under maintenace
                //    isMaintenance = MaintenanceLogic.IsWebMaintenanceMode();

                //    if (isMaintenance)
                //    { break; }

                //    if (SessionManager.AdminSession != null)
                //    {
                //        try
                //        {
                //            session = SessionManager.AdminSession;
                //            cached = SessionCache.GetSessionCacheByKey<AdminSessionCacheModel>(session.ASId);

                //            if (cached == null)
                //            { break; }

                //            //render UI

                //            litLoginName.Text = session.FullName;
                //            litLoginRole.Text = "Administrator";
                //            litGreeting.Text = $"Hi {session.FirstName}";

                //            //set boolean
                //            isAllowAccessBool = true;
                //        }
                //        catch { }
                //    }

                //    break;
                //}

                //if (!isAllowAccessBool)
                //{
                //    if (session != null)
                //    {
                //        //logout
                //        LoginLogic.LogoutAccount(session.ASId, out RespArgs<Guid?> respObj);
                //    }

                //    //session purge
                //    Session.Abandon();
                //    Session.Clear();

                //    if (isMaintenance)
                //    { Response.Redirect("~/Maintenance"); }
                //    else
                //    { Response.Redirect("~/SignIn"); }
                //}

                litGreeting.Text = "Welcome";

                var fileProfile = DataLogic.FileBiz.Get(SessionManager.CurrentLoginMember.ProfileFileId);
                if(fileProfile != null)
                {
                    Model.DataObject.FileDisplay fdProfile = fileProfile.ToDisplay();
                    fdProfile.SetUploadPath(DataLogic.Library.FileHelper.GetUploadFullPath(DataLogic.Library.FileHelper.FileDir.ProfileFileDir));
                    navImageProfile.Src = fdProfile.FileThumbnailPath;
                }
            }
        }

        //Events
        #region Events
        protected void lnkbLogoout_Click(object sender, EventArgs e)
        {
            SessionManager.SetLoginMember(null);
            Response.Redirect("~/SignIn");
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

        #endregion
    }
}