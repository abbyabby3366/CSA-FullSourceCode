using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class AdminNewViewModel
    {
        public AdminNewViewModel(List<ValueText<int>> roleList, List<hudanLibrary.Data.SelectItem<int>> teams)
        {
            RoleList = roleList;
            Teams = teams;
        }

        public List<ValueText<int>> RoleList { get; set; }
        public List<hudanLibrary.Data.SelectItem<int>> Teams { get; set; }
    }
}
