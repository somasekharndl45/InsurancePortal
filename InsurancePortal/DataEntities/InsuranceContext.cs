using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InsurancePortal.DataEntities
{
    public partial class InsuranceContext : DbContext
    {
        public InsuranceContext()
        {
        }

        public InsuranceContext(DbContextOptions<InsuranceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MemberRegistration> MemberRegistrations { get; set; } = null!;
        public virtual DbSet<PolicySubmission> PolicySubmissions { get; set; } = null!;
        public virtual DbSet<UserRegistration> UserRegistrations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=CTSDOTNET668;Initial Catalog=Insurance;User ID=sa;Password=pass@word1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberRegistration>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK__MemberRe__0CF04B38F5158E56");

                entity.ToTable("MemberRegistration");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DateofBirth).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PolicySubmission>(entity =>
            {
                entity.HasKey(e => e.PolicyId)
                    .HasName("PK__PolicySu__2E133944A89C7608");

                entity.ToTable("PolicySubmission");

                entity.Property(e => e.PolicyId).HasColumnName("PolicyID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.PolicyEffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.PolicyStatus)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PolicyType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PremiumAmount).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.PolicySubmissions)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__PolicySub__Membe__36B12243");
            });

            modelBuilder.Entity<UserRegistration>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserRegi__1788CC4C48F24068");

                entity.ToTable("UserRegistration");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserRole)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
