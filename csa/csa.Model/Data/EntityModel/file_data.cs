namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("file_data")]
    public partial class file_data
    {
        public Guid Id { get; set; }

        public double Size { get; set; }

        [StringLength(200)]
        public string Filename { get; set; }

        [StringLength(50)]
        public string Extension { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
