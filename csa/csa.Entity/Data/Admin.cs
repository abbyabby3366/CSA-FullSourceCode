using csa.Model.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsaModel
{
    partial class Admin
    {
        public LoginAdmin Convert(string access)
        {
            return new LoginAdmin(this.AdminId,this.Name,this.Email,this.IsSuperAdmin,access);
        }

        public AdminDetails ConvertToAdminDetails()
        {
            return new AdminDetails(this.AdminId, this.Name, this.Email, this.RoleId,this.StatusId,this.IsSuperAdmin,this.CreateDate);
        }
    }
}
