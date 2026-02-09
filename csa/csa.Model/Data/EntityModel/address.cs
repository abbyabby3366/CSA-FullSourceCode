namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("address")]
    public partial class address
    {
        public Guid Id { get; set; }

        public int Type { get; set; }

        public Guid? UserId { get; set; }

        [StringLength(255)]
        public string Street1 { get; set; }

        [StringLength(255)]
        public string Street2 { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string Address { get; set; }

        [StringLength(255)]
        public string City { get; set; }

        [StringLength(20)]
        public string Postcode { get; set; }

        public Guid? StateId { get; set; }

        [StringLength(255)]
        public string State { get; set; }

        public Guid CountryId { get; set; }

        public int Status { get; set; }

        [ForeignKey("UserId")]
        public virtual user UserData { get; set; }

        [ForeignKey("CountryId")]
        public virtual country CountryData { get; set; }

        [ForeignKey("StateId")]
        public virtual state StateData { get; set; }
    }
}
