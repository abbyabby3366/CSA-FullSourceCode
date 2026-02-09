using System;

namespace csa.Model
{
    public class AgentModel
    {

    }

    //================================================================================================

    public class AgentGVByMemberModel
    {
        public string DT_RowId { get; set; }

        public int Type { get; set; }

        public string AgentAccNo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Remark { get; set; }

        public double WalletAmount { get; set; }

        public int Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        //public Guid? CreatedBy { get; set; }

        //public DateTime? UpdatedDate { get; set; }

        //public DateTime? UpdatedBy { get; set; }

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
