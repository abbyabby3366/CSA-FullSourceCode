using csa.DataLogic;
using csa.Model.DataObject;
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
    public partial class AudienceTagDetails : BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string tagIdText = Request.Params["id"];
                int tagId = 0;
                if(int.TryParse(tagIdText,out tagId)) {
                    LoadData(tagId);
                }
                else
                {
                    Response.Redirect("~/AudienceTag");
                }
            }
        }

        private void LoadData(int id)
        {
            var tag = TagBiz.Get(id);
            if(tag == null) Response.Redirect("~/AudienceTag");
            var vm = new AudienceTagDetailAdminViewModel(new TagDetails(tag.TagId,tag.Label,tag.StatusID));
            hfModelView.Value = JsonConvert.SerializeObject(vm);
        }
    }
}