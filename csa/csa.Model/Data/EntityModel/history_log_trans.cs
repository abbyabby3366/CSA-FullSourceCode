namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("history_log_trans")]
    public partial class history_log_trans
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public history_log_trans()
        {

        }

        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SequenceId { get; set; }

        public int TransTypeId { get; set; }

        public Guid? UserId { get; set; }

        public Guid? OrderTransId { get; set; }

        public Guid? WalletTransId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        [ForeignKey("UserId")]
        public virtual user UserData { get; set; }
    }
}
