using System;
using Data_Access_Layer.Users.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data_Access_Layer.Users
{
    public partial class UsersMovieContext : IdentityDbContext<ApplicationUser>
    {
        public UsersMovieContext()
        {
        }

        public UsersMovieContext(DbContextOptions<UsersMovieContext> options)
                        : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserAppPermission> UserAppPermission { get; set; }
        public virtual DbSet<UserCredential> UserCredentials { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Phone1).HasMaxLength(50);

                entity.Property(e => e.Phone2).HasMaxLength(50);

                entity.HasOne(d => d.ApplicationUser)
                   .WithOne(p => p.User)
                   .HasForeignKey<User>(d => d.Id)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("FK_AplicationUser_User");
            });

            modelBuilder.Entity<UserAppPermission>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("UserAppPermission");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.ApplicationUser)
                    .WithOne(p => p.UserAppPermission)
                    .HasForeignKey<UserAppPermission>(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AplicationUser_UserAppPremission");
            });

            modelBuilder.Entity<UserCredential>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Username, "UQ__UserCred__536C85E40C37D47C")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Username).HasMaxLength(256);

                entity.HasOne(d => d.ApplicationUser)
                    .WithOne(p => p.UserCredential)
                    .HasForeignKey<UserCredential>(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AplicationUser_UserCredential");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
