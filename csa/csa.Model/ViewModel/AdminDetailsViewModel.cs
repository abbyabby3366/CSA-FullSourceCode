using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class AdminDetailsViewModel
    {
        public AdminDetailsViewModel(List<ValueText<int>> roleList, AdminDetails admin)
        {
            RoleList = roleList;
            Admin = admin;
        }

        public List<ValueText<int>> RoleList { get; set; }
        public AdminDetails Admin { get; set; }
    }
}
