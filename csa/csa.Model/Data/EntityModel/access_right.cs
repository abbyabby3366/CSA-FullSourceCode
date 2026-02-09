namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("access_right")]
    public partial class access_right
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public access_right()
        {

        }

        public Guid Id { get; set; }

        public Guid RoleId { get; set; }

        public int MenuId { get; set; }

        public long Authority { get; set; }

        //[ForeignKey("RoleId")]
        //public virtual role RoleData { get; set; }
    }
}
