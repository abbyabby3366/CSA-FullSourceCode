namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("meta_tag")]
    public partial class meta_tag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public meta_tag()
        {
            
        }

        public Guid Id { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public long SequenceId { get; set; }

        public int Type { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string Remark { get; set; }

        public int TagGrpInd { get; set; }

        public int OrderSeq { get; set; }

        public int Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}
