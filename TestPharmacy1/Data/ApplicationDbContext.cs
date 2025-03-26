using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestPharmacy1.Models;

namespace TestPharmacy1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Medication> Medication { get; set; }
        public DbSet<ConsistencyOfMedication> ConsistencyOfMedication { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<TypeOfMedication> TypeOfMedication { get; set; }
        public DbSet<OwnedMedication> OwnedMedication { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Medication>()
                .HasOne(o => o.TypeOfMedication)
                .WithOne(o => o.Medication)
                .HasPrincipalKey<TypeOfMedication>(o => o.Id)
                .HasForeignKey<Medication>(o => o.TypeOfMedicationId);
            builder.Entity<Medication>()
                .HasOne(o => o.ConsistencyOfMedication)
                .WithOne(o => o.Medication)
                .HasPrincipalKey<ConsistencyOfMedication>(o => o.Id)
                .HasForeignKey<Medication>(o => o.ConsistencyOfMedicationId);
            builder.Entity<OwnedMedication>()
                .HasOne(o => o.Medication)
                .WithOne(o => o.OwnedMedication)
                .HasPrincipalKey<Medication>(o => o.Id)
                .HasForeignKey<OwnedMedication>(o =>o.MedId);
            builder.Entity<OwnedMedication>()
                .HasOne(o => o.User)
                .WithOne(o => o.OwnedMedication)
                .HasPrincipalKey<ApplicationUser>(o=>o.Id)
                .HasForeignKey<OwnedMedication>(o => o.UserId);
            builder.Entity<Prescription>()
                .HasOne(o => o.User)
                .WithMany(o => o.Prescriptions)
                .HasForeignKey(o => o.UserId);
        }
    }
}
