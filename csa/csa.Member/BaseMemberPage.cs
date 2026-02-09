using csa.Member.Helpers;
using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace csa.Member
{
    public class BaseMemberPage : System.Web.UI.Page
    {
        protected static LoginMember CurrentLoginMember => SessionManager.CurrentLoginMember;
    }
}