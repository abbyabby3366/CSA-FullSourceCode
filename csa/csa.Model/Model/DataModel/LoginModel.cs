using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using csa.Library;

namespace csa.Model
{
    public class LoginMemberModel
    {
        public Guid USId { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public FileDataModel ImageData { get; set; }

        public int AccountType { get; set; }

        public int Status { get; set; }

        public DateTime? LastLogin { get; set; }

        //extra

        //public double Wallet1Amount { get; set; }

        //add-on

        public string FullName
        {
            get
            {
                return $"{this.FirstName}{(string.IsNullOrEmpty(this.LastName) ? string.Empty : string.Format(" {0}", this.LastName))}";
            }
        }

        public string ImageUrl
        {
            get
            {
                if (ImageData == null)
                { return null; }
                else
                { return System.IO.Path.Combine($"{Constant.BASE_FILE_PATH}{Constant.USER_IMG}{ImageData.Id}{ImageData.Extension}"); }
            }
        }
    }

    //================================================================================================

    public class LoginAdminModel
    {
        public Guid ASId { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public FileDataModel ImageData { get; set; }

        public int AccountType { get; set; }

        public int Status { get; set; }

        public DateTime? LastLogin { get; set; }

        //add-on

        public string FullName
        {
            get
            {
                return $"{this.FirstName}{(string.IsNullOrEmpty(this.LastName) ? string.Empty : string.Format(" {0}", this.LastName))}";
            }
        }

        public string ImageUrl
        {
            get
            {
                if (ImageData == null)
                { return null; }
                else
                { return System.IO.Path.Combine($"{Constant.BASE_FILE_PATH}{Constant.USER_IMG}{ImageData.Id}{ImageData.Extension}"); }
            }
        }
    }
}
