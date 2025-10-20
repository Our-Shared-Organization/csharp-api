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

    public virtual DbSet<Master> Masters { get; set; }

    public virtual DbSet<MasterService> MasterServices { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

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

            entity.HasIndex(e => e.BookingUserLogin, "booking___fk");

            entity.HasIndex(e => e.BookingMasterId, "booking_master_masterId_fk");

            entity.HasIndex(e => e.BookingServiceId, "booking_service_serviceId_fk");

            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.BookingBookedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("bookingBookedAt");
            entity.Property(e => e.BookingFinish)
                .HasColumnType("timestamp")
                .HasColumnName("bookingFinish");
            entity.Property(e => e.BookingMasterId).HasColumnName("bookingMasterId");
            entity.Property(e => e.BookingServiceId).HasColumnName("bookingServiceId");
            entity.Property(e => e.BookingStart)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("bookingStart");
            entity.Property(e => e.BookingStatus)
                .HasColumnType("enum('booked','confirmed','executing','finished','canceled')")
                .HasColumnName("bookingStatus");
            entity.Property(e => e.BookingUserLogin)
                .HasMaxLength(45)
                .HasColumnName("bookingUserLogin");

            entity.HasOne(d => d.BookingMaster).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BookingMasterId)
                .HasConstraintName("booking_master_masterId_fk");

            entity.HasOne(d => d.BookingService).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BookingServiceId)
                .HasConstraintName("booking_service_serviceId_fk");

            entity.HasOne(d => d.BookingUserLoginNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BookingUserLogin)
                .HasConstraintName("booking_user_userLogin_fk");
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

        modelBuilder.Entity<Master>(entity =>
        {
            entity.HasKey(e => e.MasterId).HasName("PRIMARY");

            entity.ToTable("master");

            entity.HasIndex(e => e.MasterUserLogin, "master_user_userId_fk");

            entity.Property(e => e.MasterId).HasColumnName("masterId");
            entity.Property(e => e.MasterExperience)
                .HasComment("Опыт работы в месяцах")
                .HasColumnName("masterExperience");
            entity.Property(e => e.MasterSpecialization)
                .HasColumnType("text")
                .HasColumnName("masterSpecialization");
            entity.Property(e => e.MasterStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("masterStatus");
            entity.Property(e => e.MasterUserLogin)
                .HasMaxLength(45)
                .HasColumnName("masterUserLogin");

            entity.HasOne(d => d.MasterUserLoginNavigation).WithMany(p => p.Masters)
                .HasForeignKey(d => d.MasterUserLogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("master_user_userLogin_fk");
        });

        modelBuilder.Entity<MasterService>(entity =>
        {
            entity.HasKey(e => e.MsId).HasName("PRIMARY");

            entity.ToTable("master_service");

            entity.HasIndex(e => e.MsMasterId, "master_service_master_masterId_fk");

            entity.HasIndex(e => e.MsServiceId, "master_service_service_serviceId_fk");

            entity.Property(e => e.MsId).HasColumnName("msId");
            entity.Property(e => e.MsMasterId).HasColumnName("msMasterId");
            entity.Property(e => e.MsServiceId).HasColumnName("msServiceId");

            entity.HasOne(d => d.MsMaster).WithMany(p => p.MasterServices)
                .HasForeignKey(d => d.MsMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("master_service_master_masterId_fk");

            entity.HasOne(d => d.MsService).WithMany(p => p.MasterServices)
                .HasForeignKey(d => d.MsServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("master_service_service_serviceId_fk");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PRIMARY");

            entity.ToTable("rating");

            entity.HasIndex(e => e.RatingId, "ratingId").IsUnique();

            entity.HasIndex(e => e.RatingMasterId, "rating_master_masterId_fk");

            entity.HasIndex(e => e.RatingUserLogin, "rating_user_userLogin_fk");

            entity.Property(e => e.RatingId).HasColumnName("ratingId");
            entity.Property(e => e.RatingMasterId).HasColumnName("ratingMasterId");
            entity.Property(e => e.RatingStars)
                .HasDefaultValueSql("'5'")
                .HasColumnName("ratingStars");
            entity.Property(e => e.RatingText)
                .HasColumnType("text")
                .HasColumnName("ratingText");
            entity.Property(e => e.RatingUserLogin)
                .HasMaxLength(45)
                .HasColumnName("ratingUserLogin");

            entity.HasOne(d => d.RatingMaster).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.RatingMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rating_master_masterId_fk");

            entity.HasOne(d => d.RatingUserLoginNavigation).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.RatingUserLogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rating_user_userLogin_fk");
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
            entity.HasKey(e => e.UserLogin).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.UserRoleId, "user_role_roleId_fk");

            entity.Property(e => e.UserLogin)
                .HasMaxLength(45)
                .HasColumnName("userLogin");
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
