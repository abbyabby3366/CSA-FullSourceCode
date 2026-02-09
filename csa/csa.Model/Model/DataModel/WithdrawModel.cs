using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model
{
    public class WithdrawModel
    {

    }

    //================================================================================================

    public class WithdrawGVByAdminModel
    {
        public string DT_RowId { get; set; }

        public Guid MemberId { get; set; }

        //public string Email { get; set; }

        //public string FirstName { get; set; }

        //public string LastName { get; set; }

        //public string PhoneNo { get; set; }

        public string BankAccNo { get; set; }

        public double Amount { get; set; }

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

    public class WithdrawExportExcelByAdminModel
    {
        
    }

    //================================================================================================

    public class WithdrawByAdminModel
    {
        
    }
}
