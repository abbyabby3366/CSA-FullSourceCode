namespace csa.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using csa.Data.EntityModel;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=dbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<access_right> access_right { get; set; }
        public virtual DbSet<address> addresses { get; set; }
        public virtual DbSet<application> applications { get; set; }
        public virtual DbSet<campaign> campaigns { get; set; }
        public virtual DbSet<changes_log> changes_log { get; set; }
        public virtual DbSet<country> countries { get; set; }
        public virtual DbSet<email_log> email_log { get; set; }
        public virtual DbSet<file_data> file_data { get; set; }
        public virtual DbSet<group> groups { get; set; }
        public virtual DbSet<history_log_trans> history_log_trans { get; set; }
        public virtual DbSet<login_log> login_log { get; set; }
        public virtual DbSet<meta_tag> meta_tags { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<scheduler_log> scheduler_log { get; set; }
        public virtual DbSet<setting> settings { get; set; }
        public virtual DbSet<state> states { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<user_session> user_session { get; set; }
        public virtual DbSet<user_vault> user_vault { get; set; }
        public virtual DbSet<wallet_trans> wallet_trans { get; set; }
        public virtual DbSet<withdraw> withdraws { get; set; }

        //view
        public virtual DbSet<v_user_vault> v_user_vault { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<address>()
                .Property(e => e.Street1)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.Street2)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<address>()
                .Property(e => e.Postcode)
                .IsUnicode(false);

            modelBuilder.Entity<country>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<country>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<country>()
                .Property(e => e.Currency)
                .IsUnicode(false);

            modelBuilder.Entity<country>()
                .Property(e => e.IsoNumeric)
                .IsUnicode(false);

            modelBuilder.Entity<country>()
                .Property(e => e.IsoAlpha3)
                .IsUnicode(false);

            modelBuilder.Entity<login_log>()
                .Property(e => e.Browser)
                .IsUnicode(false);

            modelBuilder.Entity<login_log>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<setting>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<setting>()
                .Property(e => e.StrValue)
                .IsUnicode(false);

            modelBuilder.Entity<setting>()
                .Property(e => e.TextValue)
                .IsUnicode(false);

            modelBuilder.Entity<state>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<state>()
                .Property(e => e.Name)
                .IsUnicode(false);

            //modelBuilder.Entity<state>()
            //    .HasMany(e => e.addresses)
            //    .WithRequired(e => e.state)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.ICNumber)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.TimeZone)
                .IsUnicode(false);

            //modelBuilder.Entity<user>()
            //    .HasMany(e => e.customer)
            //    .WithOptional(e => e.UserData)
            //    .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<user_session>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);

            modelBuilder.Entity<user_vault>()
                .Property(e => e.Salt)
                .IsUnicode(false);

            modelBuilder.Entity<user_vault>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<user_vault>()
                .Property(e => e.Salt2ndPass)
                .IsUnicode(false);

            modelBuilder.Entity<user_vault>()
                .Property(e => e.SecondaryPassword)
                .IsUnicode(false);

            modelBuilder.Entity<v_user_vault>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<v_user_vault>()
                .Property(e => e.Salt)
                .IsUnicode(false);

            modelBuilder.Entity<v_user_vault>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
