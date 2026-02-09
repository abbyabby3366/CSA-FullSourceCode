using System;

namespace csa.Model
{
    public class RoleModel
    {

    }

    //================================================================================================

    public class AdminRoleGVByAdminModel
    {
        public string DT_RowId { get; set; }

        //public Guid RoleId { get; set; }

        public string RoleName { get; set; }

        public string RoleDesc { get; set; }

        public int Status { get; set; }
    }

    //================================================================================================

    public class AdminRoleUsersGVByAdminModel
    {
        public string DT_RowId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ICNumber { get; set; }

        public string PhoneNo { get; set; }

        public int AccountType { get; set; }

        public Guid? RoleId { get; set; }

        public string RoleName { get; set; }

        //add-on

        public string FullName
        {
            get
            {
                return $"{this.FirstName}{(string.IsNullOrEmpty(this.LastName) ? string.Empty : string.Format(" {0}", this.LastName))}";
            }
        }
    }
}