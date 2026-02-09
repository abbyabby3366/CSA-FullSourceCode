using csa.DataLogic;
using csa.Library;
using csa.Model.DataObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csa.Member
{
    public partial class ApplicationDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string idText = Request.Params["id"];

                if(long.TryParse(idText,out var id))
                {
                    SetEdit(id);
                }
                else
                {
                    Response.Redirect("ApplicationStatus.aspx");
                }
            }
        }

        void SetEdit(long id)
        {
            var findApplication = ApplicationBiz.Get(id);
            if (findApplication == null) Response.Redirect("ApplicationStatus.aspx");
            //if(!new int[] { (int)csa.Library.ApplicationStatus.PRE_CHECKING }.Contains(findApplication.ApplicationStatusId)) Response.Redirect("ApplicationStatus.aspx");
            hfModelView.Value = JsonConvert.SerializeObject(new ApplicationDetailsMember(findApplication.ApplicationId, findApplication.ApplicationStatusId));
        }
    }
}