using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyProtocolsAPI_DanielRamírez.Models
{
    public partial class MyProtocolsDBContext : DbContext
    {
        public MyProtocolsDBContext()
        {
        }

        public MyProtocolsDBContext(DbContextOptions<MyProtocolsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Protocol> Protocols { get; set; } = null!;
        public virtual DbSet<ProtocolCategory> ProtocolCategories { get; set; } = null!;
        public virtual DbSet<ProtocolStep> ProtocolSteps { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("SERVER=.; DATABASE=MyProtocolsDB; INTEGRATED SECURITY=FALSE; USER ID=MyProtocolsAPIUser; PASSWORD=123456;");
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Protocol>(entity =>
            {
                entity.ToTable("Protocol");

                entity.Property(e => e.ProtocolId).HasColumnName("ProtocolID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.AlarmActive)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.AlarmHour).HasColumnType("time(0)");

                entity.Property(e => e.AlarmJustInWeekDays)
                    .IsRequired()
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(1500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ProtocolCategoryNavigation)
                    .WithMany(p => p.Protocols)
                    .HasForeignKey(d => d.ProtocolCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKProtocol810482");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Protocols)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKProtocol234132");

                entity.HasMany(d => d.ProtocolStepProtocolSteps)
                    .WithMany(p => p.ProtocolProtocols)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProtocolDetail",
                        l => l.HasOne<ProtocolStep>().WithMany().HasForeignKey("ProtocolStepProtocolStepsId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKProtocolDe752591"),
                        r => r.HasOne<Protocol>().WithMany().HasForeignKey("ProtocolProtocolId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FKProtocolDe996135"),
                        j =>
                        {
                            j.HasKey("ProtocolProtocolId", "ProtocolStepProtocolStepsId").HasName("PK__Protocol__7C26129531736703");

                            j.ToTable("ProtocolDetail");

                            j.IndexerProperty<int>("ProtocolProtocolId").HasColumnName("ProtocolProtocolID");

                            j.IndexerProperty<int>("ProtocolStepProtocolStepsId").HasColumnName("ProtocolStepProtocolStepsID");
                        });
            });

            modelBuilder.Entity<ProtocolCategory>(entity =>
            {
                entity.HasKey(e => e.ProtocolCategory1)
                    .HasName("PK__Protocol__0ED5AEE5D75843A7");

                entity.ToTable("ProtocolCategory");

                entity.Property(e => e.ProtocolCategory1).HasColumnName("ProtocolCategory");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProtocolCategories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKProtocolCa189942");
            });

            modelBuilder.Entity<ProtocolStep>(entity =>
            {
                entity.HasKey(e => e.ProtocolStepsId)
                    .HasName("PK__Protocol__C75F186E8944029D");

                entity.ToTable("ProtocolStep");

                entity.Property(e => e.ProtocolStepsId).HasColumnName("ProtocolStepsID");

                entity.Property(e => e.Description)
                    .HasMaxLength(1500)
                    .IsUnicode(false);

                entity.Property(e => e.Step)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProtocolSteps)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKProtocolSt628473");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.Address)
                    .HasMaxLength(1500)
                    .IsUnicode(false);

                entity.Property(e => e.BackUpEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsBlocked)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUser854768");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
