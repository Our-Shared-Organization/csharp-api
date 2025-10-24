using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
        => optionsBuilder.UseNpgsql("Host=ep-billowing-tree-a96skbq3-pooler.gwc.azure.neon.tech;Port=5432;Database=spasalon;Username=spasalon;Password=npg_MbgnFVa0Ks6o");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("bookingstatus", new[] { "booked", "confirmed", "executing", "finished", "canceled" })
            .HasPostgresEnum("usersex", new[] { "male", "female" });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Bookingid).HasName("booking_pk");

            entity.ToTable("booking");

            entity.Property(e => e.Bookingid)
                .ValueGeneratedNever()
                .HasColumnName("bookingid");
            entity.Property(e => e.Bookingbookedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("bookingbookedat");
            entity.Property(e => e.Bookingfinish)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("bookingfinish");
            entity.Property(e => e.Bookingmasterid).HasColumnName("bookingmasterid");
            entity.Property(e => e.Bookingserviceid).HasColumnName("bookingserviceid");
            entity.Property(e => e.Bookingstatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (Bookingstatus)Enum.Parse(typeof(Bookingstatus), v))
                .IsUnicode(false);
            entity.Property(e => e.Bookingstart)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("bookingstart");
            entity.Property(e => e.Bookinguserlogin)
                .HasMaxLength(45)
                .HasColumnName("bookinguserlogin");

            entity.HasOne(d => d.Bookingmaster).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Bookingmasterid)
                .HasConstraintName("booking_master_masterid_fk");

            entity.HasOne(d => d.Bookingservice).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Bookingserviceid)
                .HasConstraintName("booking_service_serviceid_fk");

            entity.HasOne(d => d.BookinguserloginNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Bookinguserlogin)
                .HasConstraintName("booking_user_userlogin_fk");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("category_pk");

            entity.ToTable("category");

            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Categorydescription).HasColumnName("categorydescription");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(255)
                .HasColumnName("categoryname");
            entity.Property(e => e.Categorystatus)
                .HasDefaultValue(true)
                .HasColumnName("categorystatus");
        });

        modelBuilder.Entity<Master>(entity =>
        {
            entity.HasKey(e => e.Masterid).HasName("master_pk");

            entity.ToTable("master");

            entity.Property(e => e.Masterid).HasColumnName("masterid");
            entity.Property(e => e.Masterexperience)
                .HasDefaultValue(0)
                .HasColumnName("masterexperience");
            entity.Property(e => e.Masterspecialization).HasColumnName("masterspecialization");
            entity.Property(e => e.Masterstatus)
                .HasDefaultValue(true)
                .HasColumnName("masterstatus");
            entity.Property(e => e.Masteruserlogin)
                .HasMaxLength(45)
                .HasColumnName("masteruserlogin");

            entity.HasOne(d => d.MasteruserloginNavigation).WithMany(p => p.Masters)
                .HasForeignKey(d => d.Masteruserlogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("master_user_userlogin_fk");
        });

        modelBuilder.Entity<MasterService>(entity =>
        {
            entity.HasKey(e => e.Msid).HasName("master_service_pk");

            entity.ToTable("master_service");

            entity.Property(e => e.Msid).HasColumnName("msid");
            entity.Property(e => e.Msmasterid).HasColumnName("msmasterid");
            entity.Property(e => e.Msserviceid).HasColumnName("msserviceid");

            entity.HasOne(d => d.Msmaster).WithMany(p => p.MasterServices)
                .HasForeignKey(d => d.Msmasterid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("master_service_master_masterid_fk");

            entity.HasOne(d => d.Msservice).WithMany(p => p.MasterServices)
                .HasForeignKey(d => d.Msserviceid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("master_service_service_serviceid_fk");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Ratingid).HasName("rating_pk");

            entity.ToTable("rating");

            entity.Property(e => e.Ratingid).HasColumnName("ratingid");
            entity.Property(e => e.Ratingmasterid).HasColumnName("ratingmasterid");
            entity.Property(e => e.Ratingstars)
                .HasDefaultValue(5)
                .HasColumnName("ratingstars");
            entity.Property(e => e.Ratingtext).HasColumnName("ratingtext");
            entity.Property(e => e.Ratinguserlogin)
                .HasMaxLength(45)
                .HasColumnName("ratinguserlogin");

            entity.HasOne(d => d.Ratingmaster).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.Ratingmasterid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rating_master_masterid_fk");

            entity.HasOne(d => d.RatinguserloginNavigation).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.Ratinguserlogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rating_user_userlogin_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("role_pk");

            entity.ToTable("role");

            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Rolename)
                .HasMaxLength(255)
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Serviceid).HasName("service_pk");

            entity.ToTable("service");

            entity.Property(e => e.Serviceid).HasColumnName("serviceid");
            entity.Property(e => e.Servicecategoryid).HasColumnName("servicecategoryid");
            entity.Property(e => e.Servicedescription).HasColumnName("servicedescription");
            entity.Property(e => e.Serviceduration)
                .HasDefaultValue(0)
                .HasColumnName("serviceduration");
            entity.Property(e => e.Servicename)
                .HasMaxLength(255)
                .HasColumnName("servicename");
            entity.Property(e => e.Serviceprice)
                .HasPrecision(5, 2)
                .HasColumnName("serviceprice");
            entity.Property(e => e.Servicestatus)
                .HasDefaultValue(true)
                .HasColumnName("servicestatus");

            entity.HasOne(d => d.Servicecategory).WithMany(p => p.Services)
                .HasForeignKey(d => d.Servicecategoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("service_category_categoryid_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userlogin).HasName("user_pk");

            entity.ToTable("user");

            entity.Property(e => e.Userlogin)
                .HasMaxLength(45)
                .HasColumnName("userlogin");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
            entity.Property(e => e.Userpassword).HasColumnName("userpassword");
            entity.Property(e => e.Userphone)
                .HasMaxLength(12)
                .HasColumnName("userphone");
            entity.Property(e => e.Usersex)
                .HasConversion(
                    v => v.ToString(),
                    v => (Usersex)Enum.Parse(typeof(Usersex), v))
                .IsUnicode(false);
            entity.Property(e => e.Userroleid)
                .HasDefaultValue(1)
                .HasColumnName("userroleid");
            entity.Property(e => e.Userstatus)
                .HasDefaultValue(true)
                .HasColumnName("userstatus");
            entity.Property(e => e.Usersurname)
                .HasMaxLength(255)
                .HasColumnName("usersurname");

            entity.HasOne(d => d.Userrole).WithMany(p => p.Users)
                .HasForeignKey(d => d.Userroleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_roleid_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
