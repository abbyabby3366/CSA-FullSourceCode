using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class Survey1DisplayViewModel
    {
        public Survey1DisplayViewModel(string answer, List<StateDisplay> states, List<DropdownItem> sectors, List<Dropdown3> jobPositions)
        {
            Answer = answer;
            States = states;
            Sectors = sectors;
            JobPositions = jobPositions;
        }

        public string Answer { get; set; }
        public List<StateDisplay> States { get; set; }
        public List<DropdownItem> Sectors { get; set; }
        public List<Dropdown3> JobPositions { get; set; }
    }
}
