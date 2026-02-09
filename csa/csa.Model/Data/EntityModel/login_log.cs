namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("login_log")]
    public partial class login_log
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime LoginDateTime { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string Browser { get; set; }

        [StringLength(50)]
        public string IPAddress { get; set; }
    }
}
