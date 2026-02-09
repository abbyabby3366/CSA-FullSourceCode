namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting")]
    public partial class setting
    {
        public Guid Id { get; set; }

        public int Ind { get; set; }

        [StringLength(100)]
        public string Code { get; set; }

        [StringLength(500)]
        public string StrValue { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string TextValue { get; set; }
    }
}
