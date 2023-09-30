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

        public virtual DbSet<QTTS01_ChangePasswordLog> QTTS01_ChangePasswordLogs { get; set; } = null!;
        public virtual DbSet<QTTS01_Group> QTTS01_Groups { get; set; } = null!;
        public virtual DbSet<QTTS01_MapProfileUser> QTTS01_MapProfileUsers { get; set; } = null!;
        public virtual DbSet<QTTS01_Module> QTTS01_Modules { get; set; } = null!;
        public virtual DbSet<QTTS01_Permission> QTTS01_Permissions { get; set; } = null!;
        public virtual DbSet<QTTS01_PermissionObject> QTTS01_PermissionObjects { get; set; } = null!;
        public virtual DbSet<QTTS01_Profile> QTTS01_Profiles { get; set; } = null!;
        public virtual DbSet<QTTS01_Tenant> QTTS01_Tenants { get; set; } = null!;
        public virtual DbSet<QTTS01_User> QTTS01_Users { get; set; } = null!;
        public virtual DbSet<QTTS01_Bank> QTTS01_Banks { get; set; } = null!;
        public virtual DbSet<QTTS01_FileManager> QTTS01_FileManagers { get; set; } = null!;

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
            modelBuilder.Entity<QTTS01_ChangePasswordLog>(entity =>
            {
                //entity.HasNoKey();

                entity.Property(e => e.id).ValueGeneratedNever();
                entity.ToTable("QTTS01_ChangePasswordLog");

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.new_password)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.old_password)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QTTS01_Group>(entity =>
            {
                entity.ToTable("QTTS01_Group");

                entity.Property(e => e.id).ValueGeneratedNever();

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

            modelBuilder.Entity<QTTS01_MapProfileUser>(entity =>
            {
                //entity.HasNoKey();

                entity.Property(e => e.id).ValueGeneratedNever();
                entity.ToTable("QTTS01_MapProfileUser");

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.description).HasMaxLength(150);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.QTTS01_Profile)
                   .WithMany(p => p.QTTS01_MapProfileUser)
                   .HasForeignKey(d => d.profile_id)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK__QTTS01_Map__profi__4BAC3F29");

                entity.HasOne(d => d.QTTS01_User)
                    .WithMany(p => p.QTTS01_MapProfileUser)
                    .HasForeignKey(d => d.username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__QTTS01_Map__usern__4CA06362");

            });

            modelBuilder.Entity<QTTS01_Module>(entity =>
            {
                //entity.HasNoKey();

                entity.Property(e => e.id).ValueGeneratedNever();
                entity.ToTable("QTTS01_Module");

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.description).HasMaxLength(250);

                entity.Property(e => e.display_name).HasMaxLength(150);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.module_name).HasMaxLength(150);
            });

            modelBuilder.Entity<QTTS01_Permission>(entity =>
            {
                //entity.HasNoKey();
                entity.Property(e => e.id).ValueGeneratedNever();
                entity.ToTable("QTTS01_Permission");

                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.description).HasMaxLength(150);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.object_name).HasMaxLength(150);

                entity.HasOne(d => d.QTTS01_PermissionObject)
                   .WithMany(p => p.QTTS01_Permission)
                   .HasForeignKey(d => d.permissionobject_id)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK__QTTS01_Per__permi__534D60F1");

                entity.HasOne(d => d.QTTS01_Profile)
                    .WithMany(p => p.QTTS01_Permission)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__QTTS01_Per__profi__5441852A");
            });

            modelBuilder.Entity<QTTS01_PermissionObject>(entity =>
            {
                //entity.HasNoKey();
                entity.Property(e => e.id).ValueGeneratedNever();
                entity.ToTable("QTTS01_PermissionObject");
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.description).HasMaxLength(250);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.object_name).HasMaxLength(150);
            });

            modelBuilder.Entity<QTTS01_Profile>(entity =>
            {
                //entity.HasNoKey();
                entity.Property(e => e.id).ValueGeneratedNever();
                entity.ToTable("QTTS01_Profile");
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.description).HasMaxLength(150);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.profile_name).HasMaxLength(150);
            });

            modelBuilder.Entity<QTTS01_Tenant>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();
                entity.ToTable("QTTS01_Tenant");
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

            modelBuilder.Entity<QTTS01_Bank>(entity =>
            {
                entity.ToTable("QTTS01_Bank");

                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.bank_code)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.bank_name).HasMaxLength(500);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.description).HasMaxLength(150);

                entity.Property(e => e.modify_time).HasColumnType("datetime");
            });

            modelBuilder.Entity<QTTS01_FileManager>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();
                entity.ToTable("QTTS01_FileManager");

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.file_name)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.size)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.url_file)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.object_file)
                  .HasMaxLength(50)
                  .IsUnicode(false)
                  .HasDefaultValueSql("('')");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
