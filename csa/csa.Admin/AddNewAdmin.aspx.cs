using csa.DataLogic;
using csa.Library;
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
    public partial class AddNewAdmin : BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            hfModelView.Value = JsonConvert.SerializeObject(new AdminNewViewModel(RoleBiz.Gets().Select(x=> new Model.DataObject.ValueText<int>(x.RoleId,x.Name)).ToList(), hudanLibrary.EnumHelper.EnumSource(typeof(AdminTeamType),"Select an options").ToList()));
        }
    }
}