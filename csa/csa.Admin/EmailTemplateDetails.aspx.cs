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
    public partial class EmailTemplateDetails : BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string tagIdText = Request.Params["id"];
                int tagId = 0;
                if (int.TryParse(tagIdText, out tagId))
                {
                    LoadData(tagId);
                }
                else
                {
                    Response.Redirect("~/EmailTemplate");
                }
            }
        }

        private void LoadData(int id)
        {
            var obj = EmailTemplateBiz.Get(id);
            if (obj == null) Response.Redirect("~/EmailTemplate");
            var vm = new EmailTemplateDetailAdminViewModel(new Model.DataObject.EmailTemplateDetails(obj.EmailTemplateId, obj.Name, obj.StatusId, obj.TemplateTypeId, obj.Content));
            hfModelView.Value = JsonConvert.SerializeObject(vm);
        }
    }
}