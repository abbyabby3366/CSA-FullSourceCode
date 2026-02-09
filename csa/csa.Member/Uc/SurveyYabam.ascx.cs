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

namespace csa.Member.Uc
{
    public partial class SurveyYabam : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetEdit(CsaModel.Survey survey)
        {
            var states = StateBiz.GetAllDisplay();
            states = states.Where(x => x.CountryId == 1).ToList();//malaysia
            var sectors = SectorBiz.Gets();
            var jobPositions = JobPositionBiz.Gets();
            var vm = new Survey1DisplayViewModel(survey.Answer,states, sectors.Select(x => new DropdownItem(x.SectorId.ToString(), x.Name)).ToList(), jobPositions.Select(x => new Dropdown3(x.JobPositionId.ToString(), x.Name, x.SectorId.ToString())).ToList());
            hfModelView.Value = JsonConvert.SerializeObject(vm);
        }
    }
}