using csa.DataLogic;
using csa.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csa.Member
{
    public partial class SurveyDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string idText = Request.Params["id"];
                long id = 0;

                if(long.TryParse(idText, out id))
                {
                    var survey = SurveyBiz.Get(id);
                    if(survey != null)
                    {
                        switch (survey.SurveyVersionId)
                        {
                            case (int)SurveyType.YABAM:
                                SurveyYabam.SetEdit(survey);
                                break;
                            default:
                                break;
                        }                        
                    }
                    else
                    {
                        Response.Redirect("~/Dashboard");
                    }
                }
                else
                {
                    Response.Redirect("~/Dashboard");
                }
            }
        }
    }
}