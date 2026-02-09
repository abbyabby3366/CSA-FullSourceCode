using System;

namespace csa.Model
{
    public class MemberModel
    {
        
    }

    //================================================================================================

    public class NewMemberApprovalGVByAdminModel
    {
        public string DT_RowId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ICNumber { get; set; }

        public int Gender { get; set; }

        public string PhoneNo { get; set; }

        public string CompanyName { get; set; }

        public string Occupation { get; set; }

        //public Guid? ReferrerId { get; set; }

        public string ReferrerFirstName { get; set; }

        public string ReferrerLastName { get; set; }

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

        public string ReferrerFullName
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

    //================================================================================================

    public class MemberGVByAdminModel
    {
        public string DT_RowId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ICNumber { get; set; }

        public int Gender { get; set; }

        public string PhoneNo { get; set; }

        public string CompanyName { get; set; }

        public string Occupation { get; set; }

        //public Guid? ReferrerId { get; set; }

        public string ReferrerFirstName { get; set; }

        public string ReferrerLastName { get; set; }

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

        public string ReferrerFullName
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