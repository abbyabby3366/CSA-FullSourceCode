using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.DataObject
{
    public class RequestLoginAdmin
    {
        public string Email { get; set; }
        public string Password { get; set; }        
    }

    public class LoginAdmin
    {
        public LoginAdmin(int adminId, string name, string email, int isSuperAdmin, string access)
        {
            AdminId = adminId;
            Name = name;
            Email = email;
            IsSuperAdmin = isSuperAdmin;
            Access = access;
        }

        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int IsSuperAdmin { get; set; }
        public string Access { get; set; }
    }

    public class RequestAdminChangePassword
    {
        public int AdminId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }

    public class RequestAdminChangeRole
    {
        public int AdminId { get; set; }
        public int RoleId { get; set; }
    }

    public class AdminRoleRequestGV
    {
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
        public DateTime CreateDate { get; set; }
        public int StatusId { get; set; }
        public int? RoleId { get; set; }
    }

    public class AdminRequestGV
    {
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public int StatusId { get; set; }
        public int? IsSuperAdmin { get; set; }
    }

    public class RequestNewAdmin
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int IsSuperAdmin { get; set; }
        public int? AdminTypeId { get; set; }
        public int? TeamId { get; set; }
    }

    public class RequestUpdateAdmin
    {
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
    }

    public class AdminDetails
    {
        public AdminDetails(int adminId, string name, string email, int? roleId, int statusId, int isSuperAdmin, DateTime createDate)
        {
            AdminId = adminId;
            Name = name;
            Email = email;
            RoleId = roleId;
            StatusId = statusId;
            IsSuperAdmin = isSuperAdmin;
            CreateDate = createDate;
        }

        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }
        public int StatusId { get; set; }
        public int IsSuperAdmin { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class RequestAdminChangePasswordByAdmin
    {
        public int AdminId { get; set; }
        public long AdminToChangePasswordId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
