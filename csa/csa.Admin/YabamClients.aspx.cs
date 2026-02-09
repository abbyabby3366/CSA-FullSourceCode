using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csa.Admin
{
    public partial class YabamClients : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string GetLangText(string key)
        {
            var globalresourcestring = (String)GetGlobalResourceObject("Lang", key);
            return globalresourcestring;
        }
    }
}