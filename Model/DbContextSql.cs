using Common.Commons;
using Microsoft.EntityFrameworkCore;

namespace Repository.Model
{
    public partial class DbContextSql : DbContext
    {
        private string connectionString = ConfigHelper.Get("ConnectionStrings", "DefaultConnection");
        public DbContextSql()
        {
        }

        public DbContextSql(DbContextOptions<DbContextSql> options)
            : base(options)
        {
        }

        public virtual DbSet<QTTS01_Group> QTTS01_Groups { get; set; } = null!;
        public virtual DbSet<QTTS01_Tenant> QTTS01_Tenants { get; set; } = null!;
        public virtual DbSet<QTTS01_User> QTTS01_Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QTTS01_Group>(entity =>
            {
                entity.ToTable("QTTS01_Group");

                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.group_code)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.group_name).HasMaxLength(500);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");
            });

            modelBuilder.Entity<QTTS01_Tenant>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.address)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.email)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tenant_name)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<QTTS01_User>(entity =>
            {
                entity.HasKey(e => e.username)
                    .HasName("PK__QTTS01_U__F3DBC573E4D21C50");

                entity.ToTable("QTTS01_User");

                entity.Property(e => e.username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.avatar).IsUnicode(false);

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.fullname).HasMaxLength(150);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.password)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
