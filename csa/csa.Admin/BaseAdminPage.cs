using csa.Admin.Helpers;
using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace csa.Admin
{
    public class BaseAdminPage : System.Web.UI.Page
    {
        protected static LoginAdmin CurrentLoginAdmin=> SessionManager.CurrentLoginAdmin;
    }
}