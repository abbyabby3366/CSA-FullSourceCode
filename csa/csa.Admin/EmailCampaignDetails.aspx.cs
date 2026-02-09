using csa.DataLogic;
using csa.Model.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csa.Admin
{
    public partial class EmailCampaignDetails : BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idText = Request.Params["id"];
                int id = 0;
                if (int.TryParse(idText, out id))
                {
                    LoadData(id);
                }
                else
                {
                    Response.Redirect("~/EmailCampaign");
                }
            }
        }

        private void LoadData(int id)
        {
            var campaign = CampaignBiz.Get(id);
            if (campaign == null) Response.Redirect("~/EmailCampaign");
            var emailTemplates = EmailTemplateBiz.GetAllDropdown();
            var audienceGroups = AudienceGroupBiz.GetAllDropdown();

            emailTemplates.Insert(0, new Model.DataObject.DropdownItem("0", "Choose a template"));
            audienceGroups.Insert(0, new Model.DataObject.DropdownItem("0", "Choose a group"));
            hfModelView.Value = JsonConvert.SerializeObject(new CampaignDetailsViewModel(new Model.DataObject.CampaignDetails(campaign.CampaignId, campaign.StatusId, campaign.Name, campaign.Subject, campaign.EmailTemplateId,campaign.ScheduledDate, campaign.AudienceGroupId,null),emailTemplates, audienceGroups));
        }
    }
}