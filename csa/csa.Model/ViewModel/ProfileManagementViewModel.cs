using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.ViewModel
{
    public class ProfileManagementViewModel
    {
        public ProfileManagementViewModel(ProfileManagementModel profileManagementModel, List<DropdownItem> bankDropdown)
        {
            ProfileManagementModel = profileManagementModel;
            BankDropdown = bankDropdown;
        }

        public ProfileManagementModel ProfileManagementModel { get; set; }
        public List<DropdownItem> BankDropdown { get; set; }
    }
}
