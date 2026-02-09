namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user_session")]
    public partial class user_session
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime LastAccess { get; set; }

        [StringLength(50)]
        public string IPAddress { get; set; }

        [ForeignKey("UserId")]
        public virtual user UserData { get; set; }
    }
}
