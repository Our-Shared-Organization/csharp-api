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

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=mysql-whatever-livinitlarge-4d71.f.aivencloud.com;port=15134;user=apiserver;password=AVNS_xacMzzUErPnR3zhs7JN;database=spasalon", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PRIMARY");

            entity.ToTable("booking", tb => tb.HasComment("Сеанс"));

            entity.HasIndex(e => e.BookingUserId, "booking___fk");

            entity.HasIndex(e => e.BookingServiceId, "booking_service_serviceId_fk");

            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.BookingBookedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("bookingBookedAt");
            entity.Property(e => e.BookingFinish)
                .HasColumnType("timestamp")
                .HasColumnName("bookingFinish");
            entity.Property(e => e.BookingServiceId).HasColumnName("bookingServiceId");
            entity.Property(e => e.BookingStart)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("bookingStart");
            entity.Property(e => e.BookingStatus)
                .HasColumnType("enum('booked','confirmed','executing','finished','canceled')")
                .HasColumnName("bookingStatus");
            entity.Property(e => e.BookingUserId).HasColumnName("bookingUserId");

            entity.HasOne(d => d.BookingService).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BookingServiceId)
                .HasConstraintName("booking_service_serviceId_fk");

            entity.HasOne(d => d.BookingUser).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BookingUserId)
                .HasConstraintName("booking___fk");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("category");

            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.CategoryDescription)
                .HasColumnType("text")
                .HasColumnName("categoryDescription");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("categoryName");
            entity.Property(e => e.CategoryStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("categoryStatus");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PRIMARY");

            entity.ToTable("service");

            entity.HasIndex(e => e.ServiceCategoryId, "service_category_categoryId_fk");

            entity.Property(e => e.ServiceId).HasColumnName("serviceId");
            entity.Property(e => e.ServiceCategoryId).HasColumnName("serviceCategoryId");
            entity.Property(e => e.ServiceDescription)
                .HasColumnType("text")
                .HasColumnName("serviceDescription");
            entity.Property(e => e.ServiceDuration)
                .HasComment("В минутах")
                .HasColumnName("serviceDuration");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(255)
                .HasColumnName("serviceName");
            entity.Property(e => e.ServicePrice)
                .HasPrecision(5, 2)
                .HasColumnName("servicePrice");
            entity.Property(e => e.ServiceStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("serviceStatus");

            entity.HasOne(d => d.ServiceCategory).WithMany(p => p.Services)
                .HasForeignKey(d => d.ServiceCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("service_category_categoryId_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.UserRoleId, "user_role_roleId_fk");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .HasColumnName("userName");
            entity.Property(e => e.UserPassword)
                .HasColumnType("text")
                .HasColumnName("userPassword");
            entity.Property(e => e.UserPhone)
                .HasMaxLength(12)
                .HasColumnName("userPhone");
            entity.Property(e => e.UserRoleId).HasColumnName("userRoleId");
            entity.Property(e => e.UserSex)
                .HasColumnType("enum('male','female')")
                .HasColumnName("userSex");
            entity.Property(e => e.UserStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("userStatus");
            entity.Property(e => e.UserSurname)
                .HasMaxLength(255)
                .HasColumnName("userSurname");

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_roleId_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
