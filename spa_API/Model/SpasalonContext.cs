using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace whatever_api.Model;

public partial class SpasalonContext : DbContext
{
    public SpasalonContext()
    {
    }

    public SpasalonContext(DbContextOptions<SpasalonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<MasterCategory> MasterCategories { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("user=root;password=admin;server=localhost;database=db_spasalon", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Bookingid).HasName("PRIMARY");

            entity.ToTable("booking");

            entity.HasIndex(e => e.Bookingmasterlogin, "fk_booking_master");

            entity.HasIndex(e => e.Bookingserviceid, "fk_booking_service");

            entity.HasIndex(e => e.Bookinguserlogin, "fk_booking_user");

            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Bookingbookedat)
                .HasColumnType("datetime")
                .HasColumnName("bookingbookedat");
            entity.Property(e => e.Bookingfinish)
                .HasColumnType("datetime")
                .HasColumnName("bookingfinish");
            entity.Property(e => e.Bookingmasterlogin)
                .HasMaxLength(45)
                .HasColumnName("bookingmasterlogin");
            entity.Property(e => e.Bookingserviceid).HasColumnName("bookingserviceid");
            entity.Property(e => e.Bookingstart)
                .HasColumnType("datetime")
                .HasColumnName("bookingstart");
            entity.Property(e => e.Bookingstatus)
                .HasColumnType("enum('booked','confirmed','in_progress','completed','cancelled')")
                .HasColumnName("bookingstatus");
            entity.Property(e => e.Bookinguserlogin)
                .HasMaxLength(45)
                .HasColumnName("bookinguserlogin");

            entity.HasOne(d => d.BookingmasterloginNavigation).WithMany(p => p.BookingBookingmasterloginNavigations)
                .HasForeignKey(d => d.Bookingmasterlogin)
                .HasConstraintName("fk_booking_master");

            entity.HasOne(d => d.Bookingservice).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Bookingserviceid)
                .HasConstraintName("fk_booking_service");

            entity.HasOne(d => d.BookinguserloginNavigation).WithMany(p => p.BookingBookinguserloginNavigations)
                .HasForeignKey(d => d.Bookinguserlogin)
                .HasConstraintName("fk_booking_user");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("PRIMARY");

            entity.ToTable("category");

            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Categorydescription)
                .HasColumnType("text")
                .HasColumnName("categorydescription");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(255)
                .HasColumnName("categoryname");
            entity.Property(e => e.Categorystatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("categorystatus");
        });

        modelBuilder.Entity<MasterCategory>(entity =>
        {
            entity.HasKey(e => e.Mcid).HasName("PRIMARY");

            entity.ToTable("master_category");

            entity.HasIndex(e => e.Mccategoryid, "fk_mc_category");

            entity.HasIndex(e => e.Mcmasterlogin, "fk_mc_user");

            entity.Property(e => e.Mcid).HasColumnName("mcid");
            entity.Property(e => e.Mccategoryid).HasColumnName("mccategoryid");
            entity.Property(e => e.Mcmasterlogin)
                .HasMaxLength(45)
                .HasColumnName("mcmasterlogin");

            entity.HasOne(d => d.Mccategory).WithMany(p => p.MasterCategories)
                .HasForeignKey(d => d.Mccategoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_mc_category");

            entity.HasOne(d => d.McmasterloginNavigation).WithMany(p => p.MasterCategories)
                .HasForeignKey(d => d.Mcmasterlogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_mc_user");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Ratingid).HasName("PRIMARY");

            entity.ToTable("rating");

            entity.HasIndex(e => e.Ratingmasterlogin, "fk_rating_master");

            entity.HasIndex(e => e.Ratinguserlogin, "fk_rating_user");

            entity.Property(e => e.Ratingid).HasColumnName("ratingid");
            entity.Property(e => e.Ratingmasterlogin)
                .HasMaxLength(45)
                .HasColumnName("ratingmasterlogin");
            entity.Property(e => e.Ratingstars)
                .HasDefaultValueSql("'5'")
                .HasColumnName("ratingstars");
            entity.Property(e => e.Ratingtext)
                .HasColumnType("text")
                .HasColumnName("ratingtext");
            entity.Property(e => e.Ratinguserlogin)
                .HasMaxLength(45)
                .HasColumnName("ratinguserlogin");

            entity.HasOne(d => d.RatingmasterloginNavigation).WithMany(p => p.RatingRatingmasterloginNavigations)
                .HasForeignKey(d => d.Ratingmasterlogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rating_master");

            entity.HasOne(d => d.RatinguserloginNavigation).WithMany(p => p.RatingRatinguserloginNavigations)
                .HasForeignKey(d => d.Ratinguserlogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rating_user");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Rolename)
                .HasMaxLength(255)
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Serviceid).HasName("PRIMARY");

            entity.ToTable("service");

            entity.HasIndex(e => e.Servicecategoryid, "fk_service_category");

            entity.Property(e => e.Serviceid).HasColumnName("serviceid");
            entity.Property(e => e.Servicecategoryid).HasColumnName("servicecategoryid");
            entity.Property(e => e.Servicedescription)
                .HasColumnType("text")
                .HasColumnName("servicedescription");
            entity.Property(e => e.Serviceduration).HasColumnName("serviceduration");
            entity.Property(e => e.Servicename)
                .HasMaxLength(255)
                .HasColumnName("servicename");
            entity.Property(e => e.Serviceprice)
                .HasPrecision(5, 2)
                .HasColumnName("serviceprice");

            entity.HasOne(d => d.Servicecategory).WithMany(p => p.Services)
                .HasForeignKey(d => d.Servicecategoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_service_category");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userlogin).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Userroleid, "fk_user_role");

            entity.Property(e => e.Userlogin)
                .HasMaxLength(45)
                .HasColumnName("userlogin");
            entity.Property(e => e.Useremail)
                .HasMaxLength(255)
                .HasColumnName("useremail");
            entity.Property(e => e.Userexperience).HasColumnName("userexperience");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
            entity.Property(e => e.Userpassword)
                .HasColumnType("text")
                .HasColumnName("userpassword");
            entity.Property(e => e.Userphone)
                .HasMaxLength(12)
                .HasColumnName("userphone");
            entity.Property(e => e.Userroleid)
                .HasDefaultValueSql("'1'")
                .HasColumnName("userroleid");
            entity.Property(e => e.Usersex)
                .HasMaxLength(255)
                .HasColumnName("usersex");
            entity.Property(e => e.Userspecialization)
                .HasColumnType("text")
                .HasColumnName("userspecialization");
            entity.Property(e => e.Userstatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("userstatus");
            entity.Property(e => e.Usersurname)
                .HasMaxLength(255)
                .HasColumnName("usersurname");

            entity.HasOne(d => d.Userrole).WithMany(p => p.Users)
                .HasForeignKey(d => d.Userroleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
