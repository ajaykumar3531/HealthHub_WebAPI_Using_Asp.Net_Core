using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HealthHub_WebAPI.DAL.HelathHub;

public partial class HelathHubDbContext : DbContext
{
    public HelathHubDbContext()
    {
    }

    public HelathHubDbContext(DbContextOptions<HelathHubDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=HGT-LAP-371;Database=HelathHub_DB;User Id=sa;Password=Hallmark123;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Address__3214EC27BAFD1167");

            entity.ToTable("Address");

            entity.Property(e => e.Id)
                .HasMaxLength(18)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Address1).IsUnicode(false);
            entity.Property(e => e.Address2).IsUnicode(false);
            entity.Property(e => e.City).IsUnicode(false);
            entity.Property(e => e.Country).IsUnicode(false);
            entity.Property(e => e.CountryCode).IsUnicode(false);
            entity.Property(e => e.LandMark).IsUnicode(false);
            entity.Property(e => e.State).IsUnicode(false);
            entity.Property(e => e.StatePinCode).IsUnicode(false);
            entity.Property(e => e.Street).IsUnicode(false);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA2378E7EF0");

            entity.ToTable("Appointment");

            entity.Property(e => e.AppointmentId)
                .HasMaxLength(18)
                .IsFixedLength()
                .HasColumnName("AppointmentID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.DoctorUserId)
                .HasMaxLength(18)
                .IsFixedLength()
                .HasColumnName("DoctorUserID");
            entity.Property(e => e.PatientUserId)
                .HasMaxLength(18)
                .IsFixedLength()
                .HasColumnName("PatientUserID");

            entity.HasOne(d => d.DoctorUser).WithMany(p => p.AppointmentDoctorUsers)
                .HasForeignKey(d => d.DoctorUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Docto__34C8D9D1");

            entity.HasOne(d => d.PatientUser).WithMany(p => p.AppointmentPatientUsers)
                .HasForeignKey(d => d.PatientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Patie__33D4B598");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC27F56F8935");

            entity.ToTable("Department");

            entity.Property(e => e.Id)
                .HasMaxLength(18)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(18)
                .IsFixedLength();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC27CDD57B24");

            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .HasMaxLength(18)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(18)
                .IsFixedLength();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(18)
                .IsFixedLength();
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RoleName).IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC276D91D6DD");

            entity.Property(e => e.Id)
                .HasMaxLength(18)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.AddressId)
                .HasMaxLength(18)
                .IsFixedLength()
                .HasColumnName("AddressID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(18)
                .IsFixedLength();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .IsUnicode(false)
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName).IsUnicode(false);
            entity.Property(e => e.LastName).IsUnicode(false);
            entity.Property(e => e.MiddleName).IsUnicode(false);
            entity.Property(e => e.ParentUserId)
                .HasMaxLength(18)
                .IsFixedLength()
                .HasColumnName("ParentUserID");
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.PhoneNumber).IsUnicode(false);
            entity.Property(e => e.ReportTo)
                .HasMaxLength(18)
                .IsFixedLength();
            entity.Property(e => e.RoleId)
                .HasMaxLength(18)
                .IsFixedLength()
                .HasColumnName("RoleID");
            entity.Property(e => e.Specialty).IsUnicode(false);
            entity.Property(e => e.UserName).IsUnicode(false);

            entity.HasOne(d => d.Address).WithMany(p => p.Users)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__AddressID__2E1BDC42");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleID__2F10007B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
