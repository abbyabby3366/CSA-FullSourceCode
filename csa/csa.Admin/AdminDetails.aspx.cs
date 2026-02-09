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
    public partial class AdminDetails : BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var idText = Request["id"];
                if (int.TryParse(idText, out var id))
                {
                    SetEdit(id);
                }
                else
                {
                    Response.Redirect("Roles");
                }
            }
        }

        private void SetEdit(int id)
        {
            var find = AdminBiz.Get(id);
            if (find == null) Response.Redirect("Roles");

            if (find.AdminId == CurrentLoginAdmin.AdminId) Response.Redirect("MyProfile");

            hfModelView.Value = JsonConvert.SerializeObject(new AdminDetailsViewModel(RoleBiz.Gets().Select(x=> new Model.DataObject.ValueText<int>(x.RoleId,x.Name)).ToList(), find.ConvertToAdminDetails()));
        }
    }
}