using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using csa.Model;
using csa.Model.DataObject;

namespace csa.Admin.Helpers
{
    public static class SessionManager
    {
        private static LoginAdmin _loginAdmin
        {
            get
            {
                return HttpContext.Current.Session["LoginAdmin"] as LoginAdmin;
            }
            set
            {
                HttpContext.Current.Session["LoginAdmin"] = value;
            }
        }

        public static LoginAdmin SetLoginAdmin(LoginAdmin loginAdmin) => _loginAdmin = loginAdmin;
        public static LoginAdmin CurrentLoginAdmin => _loginAdmin;
    }
}