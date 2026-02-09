namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("member")]
    public partial class member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public member()
        {
            //this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SequenceId { get; set; }

        public int Type { get; set; }

        public Guid? UserId { get; set; }

        [StringLength(255)]
        public string CompanyName { get; set; }

        [StringLength(255)]
        public string Occupation { get; set; }

        public Guid? ReferrerId { get; set; }

        public int MembGrpInd { get; set; }

        public DateTime? JoinDate { get; set; }

        public int Status { get; set; }

        [ForeignKey("UserId")]
        public virtual user UserData { get; set; }

        [ForeignKey("ReferrerId")]
        public virtual user ReferrerData { get; set; }
    }
}
