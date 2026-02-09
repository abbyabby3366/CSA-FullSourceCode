namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wallet_trans")]
    public partial class wallet_trans
    {
        public Guid Id { get; set; }

        public int TransTypeId { get; set; }

        public Guid? UserId { get; set; }

        public int WalletTypeId { get; set; }

        public double Amount { get; set; }

        public double StartBalance { get; set; }

        public double EndBalance { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        [ForeignKey("UserId")]
        public virtual user UserData { get; set; }
    }
}
