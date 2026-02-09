namespace csa.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user_vault")]
    public partial class user_vault
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [StringLength(200)]
        public string Salt { get; set; }

        [StringLength(200)]
        public string Password { get; set; }

        [StringLength(200)]
        public string Salt2ndPass { get; set; }

        [StringLength(200)]
        public string SecondaryPassword { get; set; }

        [ForeignKey("UserId")]
        public virtual user UserData { get; set; }
    }
}
