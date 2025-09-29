using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace whatever_api.Model;

public partial class spaSalonDbContext : DbContext
{
    public spaSalonDbContext()
    {
    }

    public spaSalonDbContext(DbContextOptions<spaSalonDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseMySql("server=mysql-whatever-livinitlarge-4d71.f.aivencloud.com;port=15134;user=apiserver;password=AVNS_vXP2hW8u5irHGZ7swC-;database=spasalon", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PRIMARY");

            entity.ToTable("appointments");

            entity.HasIndex(e => e.ClientId, "ClientId");

            entity.HasIndex(e => e.ServiceId, "ServiceId");

            entity.HasIndex(e => e.StaffId, "appointments_staff_staffId_fk");

            entity.Property(e => e.AppointmentDate).HasColumnType("datetime");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.ServiceId).HasColumnName("serviceId");
            entity.Property(e => e.StaffId).HasColumnName("staffId");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Scheduled'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointments_ibfk_1");

            entity.HasOne(d => d.Service).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointments_ibfk_3");

            entity.HasOne(d => d.Staff).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointments_staff_staffId_fk");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PRIMARY");

            entity.ToTable("client");

            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.ClientEmail)
                .HasColumnType("text")
                .HasColumnName("clientEmail");
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .HasColumnName("clientName");
            entity.Property(e => e.ClientPassword)
                .HasColumnType("text")
                .HasColumnName("clientPassword");
            entity.Property(e => e.ClientPhone)
                .HasMaxLength(100)
                .HasColumnName("clientPhone");
            entity.Property(e => e.ClientSurname)
                .HasMaxLength(100)
                .HasColumnName("clientSurname");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("isActive");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PRIMARY");

            entity.ToTable("services");

            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Price).HasPrecision(10, 2);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PRIMARY");

            entity.ToTable("staff");

            entity.Property(e => e.StaffId).HasColumnName("staffId");
            entity.Property(e => e.StaffEmail)
                .HasColumnType("text")
                .HasColumnName("staffEmail");
            entity.Property(e => e.StaffName)
                .HasMaxLength(100)
                .HasColumnName("staffName");
            entity.Property(e => e.StaffPassword)
                .HasColumnType("text")
                .HasColumnName("staffPassword");
            entity.Property(e => e.StaffPhone)
                .HasMaxLength(100)
                .HasColumnName("staffPhone");
            entity.Property(e => e.StaffRole)
                .HasColumnType("enum('Admin','Manager')")
                .HasColumnName("staffRole");
            entity.Property(e => e.StaffSurname)
                .HasMaxLength(100)
                .HasColumnName("staffSurname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
