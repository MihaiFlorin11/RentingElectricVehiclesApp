using Microsoft.EntityFrameworkCore;
using REVA.Domain.Entities;

namespace REVA.Infrastructure.DatabaseContext
{
    public class REVADbContext(DbContextOptions<REVADbContext> options) : DbContext(options)
    {
        public DbSet<Invoice> Invoices { get; set; }
        
        public DbSet<Rental> Rentals { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(u => u.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.Property(vt => vt.Type)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(vt => vt.Description)
                    .IsRequired(false)
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.Property(vt => vt.PricePerMinute)
                    .HasPrecision(10, 2);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(i => i.TotalPrice)
                    .HasPrecision(10, 2);
            });

            modelBuilder.Entity<VehicleType>()
                .ToTable(table =>
                table.HasCheckConstraint(
                "CK_VehicleTypes_PricePerMinute_NonNegative",
                "\"PricePerMinute\" >= 0"));

            modelBuilder.Entity<Invoice>()
                .ToTable(table =>
                table.HasCheckConstraint(
                "CK_Invoices_TotalPrice_NonNegative",
                "\"TotalPrice\" >= 0"));

            modelBuilder.Entity<Rental>()
                 .ToTable(table =>
                 table.HasCheckConstraint(
                 "CK_Rentals_EndDateTime_After_StartDateTime",
                 "\"EndDateTime\" IS NULL OR \"EndDateTime\" >= \"StartDateTime\""));

            modelBuilder.Entity<Rental>()
            .ToTable(table =>
            table.HasCheckConstraint(
            "CK_Rentals_Status_Valid",
            "\"Status\" IN (1, 2, 3)"));

            modelBuilder.Entity<Invoice>()
            .ToTable(table =>
            table.HasCheckConstraint(
            "CK_Invoices_Status_Valid",
            "\"Status\" IN (1, 2, 3)"));

            modelBuilder.Entity<Vehicle>()
            .ToTable(table =>
            table.HasCheckConstraint(
            "CK_Vehicles_Status_Valid",
            "\"Status\" IN (1, 2, 3)"));

            modelBuilder.Entity<User>()
            .ToTable(table =>
            table.HasCheckConstraint(
            "CK_Users_Role_Valid",
            "\"Role\" IN (1, 2)"));

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.VehicleType)
                .WithMany(vt => vt.Vehicles)
                .HasForeignKey(v => v.VehicleTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.User)
                .WithMany(u => u.Rentals)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Vehicle)
                .WithMany(v => v.Rentals)
                .HasForeignKey(r => r.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Invoice)
                .WithOne(i => i.Rental)
                .HasForeignKey<Invoice>(i => i.RentalId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
