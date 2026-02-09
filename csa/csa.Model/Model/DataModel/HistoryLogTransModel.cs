using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model
{
    public class HistoryLogTransModel
    {

    }

    //================================================================================================

    public class WalletTransByAdminModel
    {
        public Guid Id { get; set; }

        public long SequenceId { get; set; }

        public int TransTypeId { get; set; }

        public double Amount { get; set; }

        public string UserCode { get; set; }

        public int Status { get; set; }

        public DateTime? CreatedDate { get; set; }
    }

    public class WalletTransGVByAdminModel
    {
        public string DT_RowId { get; set; }

        public long TransNo { get; set; }

        public int TransTypeId { get; set; }

        public double Amount { get; set; }

        public string UserCode { get; set; }

        public int Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        //public Guid? CreatedBy { get; set; }

        //add-on

        //public string FullName
        //{
        //    get
        //    {
        //        return $"{this.FirstName}{(string.IsNullOrEmpty(this.LastName) ? string.Empty : string.Format(" {0}", this.LastName))}";
        //    }
        //}

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
