namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("email_log")]
    public partial class email_log
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public email_log()
        {
            //member_data = new HashSet<member_data>();
        }

        public Guid Id { get; set; }

        public int Type { get; set; }

        public Guid? UserId { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string To { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string CC { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string BCC { get; set; }

        [StringLength(500)]
        public string MailSubject { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string MailContent { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string Attachment { get; set; }

        public int EmailGrpInd { get; set; }

        public int Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public Guid? UpdatedBy { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<member_data> member_data { get; set; }
    }
}
