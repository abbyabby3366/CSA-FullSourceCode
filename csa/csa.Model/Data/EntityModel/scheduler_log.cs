namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("scheduler_log")]
    public partial class scheduler_log
    {
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SequenceId { get; set; }

        public int Type { get; set; }

        public DateTime? TargetDate { get; set; }

        public string Message { get; set; }

        public int Status { get; set; }

        public DateTime? CreateDate { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}
