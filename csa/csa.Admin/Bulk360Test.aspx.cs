using csa.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csa.Admin
{
    public partial class Bulk360Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnSend_ServerClick(object sender, EventArgs e)
        {
            var bulk360 = new Bulk360(new Bulk360Message(txtFrom.Value,txtTo.Value, txtMessage.Value));
            var res = await bulk360.Send();
            txtResult.Text = JsonConvert.SerializeObject(res);
        }
    }
}