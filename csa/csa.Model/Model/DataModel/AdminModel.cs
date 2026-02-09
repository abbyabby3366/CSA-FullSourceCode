using System;
using System.Collections.Generic;

using csa.Library;

namespace csa.Model
{
    public class AdminModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public FileDataModel ImageData { get; set; }

        public IdValueModel Role { get; set; }

        public int AccountType { get; set; }

        public int Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        //add-on

        public string Image
        {
            get
            {
                if (this.ImageData != null)
                { return $"{Constant.BASE_FILE_PATH}{Constant.USER_IMG}{this.ImageData.Id}{this.ImageData.Extension}"; }
                else
                { return string.Empty; }
            }
        }

        public DateTime? CreatedDateLocal
        {
            get
            {
                if (this.CreatedDate != null) { return this.CreatedDate.Value.ToLocalTime(); }
                else { return null; }
            }
        }
    }

    public class AddEditAdminBaseModel
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public Guid? RoleId { get; set; }

        public int Status { get; set; }
    }

    public class AddAdminModel : AddEditAdminBaseModel
    {
        public string Password { get; set; }
    }

    public class EditAdminModel : AddEditAdminBaseModel
    {
        public Guid Id { get; set; }
    }

    //================================================================================================

    public class AdminGVModel
    {
        public string DT_RowId { get; set; }
        //public Guid AdminId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        //public int AccountType { get; set; }

        public int Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        //add-on

        public string FullName
        {
            get
            {
                return $"{this.FirstName}{(string.IsNullOrEmpty(this.LastName) ? string.Empty : string.Format(" {0}", this.LastName))}";
            }
        }

        public DateTime? CreatedDateLocal
        {
            get
            {
                if (this.CreatedDate != null) { return this.CreatedDate.Value.ToLocalTime(); }
                else { return null; }
            }
        }
    }
}