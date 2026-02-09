using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class AdminRolesViewModel
    {
        public AdminRolesViewModel(List<ValueText<int>> accessList, List<ValueText<int>> roleList)
        {
            AccessList = accessList;
            RoleList = roleList;
        }

        public List<ValueText<int>> AccessList { get; set; }
        public List<ValueText<int>> RoleList { get; set; }
    }
}
