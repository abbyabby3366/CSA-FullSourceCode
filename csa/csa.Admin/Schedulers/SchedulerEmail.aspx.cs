using csa.DataLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csa.Admin.Schedulers
{
    public partial class SchedulerEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string takeText = Request.Params["e"];
                if (takeText != null)
                {
                    if(int.TryParse(takeText, out int take))
                    {
                        EmailBiz.SendEmail(take, out string errorMessage);
                        Response.Write(errorMessage);
                    }
                    else
                    {
                        Response.Write("args invalid");
                    }
                }
                else
                {
                    Response.Write("args invalid");
                }
            }
        }
    }
}