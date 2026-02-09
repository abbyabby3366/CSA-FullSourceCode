using csa.DataLogic;
using csa.Library;
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
    public partial class ApplicationAssign : BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var idText = Request.Params["id"];
                if(long.TryParse(idText,out long id)) {
                    SetEdit(id);
                }
                else
                {
                    Response.Redirect("Applications.aspx");
                }
            }
        }

        private void SetEdit(long applicationId)
        {
            var findApplication = ApplicationBiz.Get(applicationId);
            if(findApplication == null) Response.Redirect("Applications.aspx");

            var member = MemberBiz.Get(findApplication.MemberId);
            var referralMember = findApplication.ReferrerMemberId.HasValue ? MemberBiz.Get(findApplication.ReferrerMemberId.Value) : null;
            var state = member.StateId.HasValue ? StateBiz.Get(member.StateId.Value) : null;
            var admins = AdminBiz.GetAllAdminActive();
            var roles = RoleBiz.Gets();
            int roleCreditTeam = roles.FirstOrDefault(x => x.AccessList.ToInt() == (int)AdminRoleType.CreditTeam).RoleId;
            int roleSalesDirector = roles.FirstOrDefault(x => x.AccessList.ToInt() == (int)AdminRoleType.SalesDirector).RoleId;
            int roleRM = roles.FirstOrDefault(x => x.AccessList.ToInt() == (int)AdminRoleType.RM).RoleId;
            int rolePA = roles.FirstOrDefault(x => x.AccessList.ToInt() == (int)AdminRoleType.PA).RoleId;

            var creditTeams = admins.Where(x => x.RoleId == roleCreditTeam).Select(x=> new ValueText<int>(x.AdminId,x.Name)).ToList();
            var salesDirector = admins.Where(x => x.RoleId == roleSalesDirector).Select(x => new ValueText<int>(x.AdminId, x.Name)).ToList();
            var rm = admins.Where(x => x.RoleId == roleRM).Select(x => new ValueText<int>(x.AdminId, x.Name)).ToList();
            var pa = admins.Where(x => x.RoleId == rolePA).Select(x => new ValueText<int>(x.AdminId, x.Name)).ToList();
            creditTeams.Insert(0,new ValueText<int>(0, "Select an option"));
            salesDirector.Insert(0,new ValueText<int>(0, "Select an option"));
            rm.Insert(0,new ValueText<int>(0, "Select an option"));
            pa.Insert(0,new ValueText<int>(0, "Select an option"));
            ValueText<long> referrerMember = null;
            if(findApplication.ReferrerMemberId.HasValue)
            {
                var findReferrer = MemberBiz.Get(findApplication.ReferrerMemberId.Value);
                referrerMember = new ValueText<long>(findApplication.ReferrerMemberId.Value, $"{findReferrer.FullName} ({findReferrer.PhoneNumber})");
            }
            hfModelView.Value = JsonConvert.SerializeObject(new ApplicationAssignViewModel(findApplication.ApplicationId,ApplicationBiz.ConvertToInfo(findApplication, member, state, referralMember), referrerMember, findApplication.PFCAdminId, findApplication.AMAdminId, findApplication.UMAdminId, findApplication.RMAdminId, findApplication.PAAdminId,creditTeams,salesDirector,rm,pa,findApplication.PreparedAdminId,findApplication.AnalyzedAdminId));
        }
    }
}