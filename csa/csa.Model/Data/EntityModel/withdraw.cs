namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("withdraw")]
    public partial class withdraw
    {
        public Guid Id { get; set; }

        public Guid MemberId { get; set; }

        public string BankName { get; set; }

        public string BankAccHolder { get; set; }

        public string BankAccNo { get; set; }

        public string TransRefNo { get; set; }

        public double Amount { get; set; }

        public int Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        [ForeignKey("MemberId")]
        public virtual member MemberData { get; set; }
    }
}
