namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("changes_log")]
    public partial class changes_log
    {
        public Guid Id { get; set; }

        public int Type { get; set; }

        public Guid? RefId { get; set; }

        [StringLength(150)]
        public string RefNo { get; set; }

        public int Table { get; set; }

        public string FromObjVal { get; set; }

        public string ToObjVal { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}
