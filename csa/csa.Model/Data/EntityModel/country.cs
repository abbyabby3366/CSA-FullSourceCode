namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("country")]
    public partial class country
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public country()
        {
            //addresses = new HashSet<address>();
            //states = new HashSet<state>();
        }

        public Guid Id { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(3)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "char")]
        [StringLength(3)]
        public string Currency { get; set; }

        [Column(TypeName = "char")]
        [StringLength(4)]
        public string IsoNumeric { get; set; }

        [Column(TypeName = "char")]
        [StringLength(3)]
        public string IsoAlpha3 { get; set; }

        public int? PhoneCode { get; set; }

        public int OrderSeq { get; set; }

        public int Status { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<address> addresses { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<state> states { get; set; }
    }
}
