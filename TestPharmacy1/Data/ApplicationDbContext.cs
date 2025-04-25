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
                .WithMany(o => o.Medication)
                .HasForeignKey(o => o.TypeOfMedicationId);
            builder.Entity<Medication>()
                .HasOne(o => o.ConsistencyOfMedication)
                .WithMany(o => o.Medication)
                .HasForeignKey(o => o.ConsistencyOfMedicationId);

            builder.Entity<OwnedMedication>()       //
                .HasOne(o => o.User)                //
                .WithMany(o => o.OwnedMedications); // 1
            builder.Entity<OwnedMedication>()       //
                .HasOne(o => o.Medication)          //
                .WithMany(o => o.OwnedMedications); //

            builder.Entity<Prescription>()
                .HasOne(o => o.User)
                .WithMany(o => o.Prescriptions)
                .HasForeignKey(o => o.UserId);

            builder.Entity<TypeOfMedication>().HasData(
               new TypeOfMedication { Id=1,Name = "Адренокортикоактивни средства" },
               new TypeOfMedication { Id = 2, Name = "Адренолитици" },
               new TypeOfMedication { Id = 3, Name = "Адреномиметици" },
               new TypeOfMedication { Id = 4, Name = "Анксиолитици (транквилизатори)" },
               new TypeOfMedication { Id=5,Name = "Антиаритмични средства" },
               new TypeOfMedication { Id=6,Name = "Антибактериалните средства" },
               new TypeOfMedication { Id=7,Name = "Антидепресанти" },
               new TypeOfMedication { Id=8,Name = "Антидиабетични средства" },
               new TypeOfMedication { Id=9,Name = "Антидиарични средства" },
               new TypeOfMedication { Id=10,Name = "Антиеметични средства" },
               new TypeOfMedication { Id=11,Name = "Антиепилептични средства" },
               new TypeOfMedication { Id=12,Name = "Антимикотични средства" },
               new TypeOfMedication { Id=13,Name = "Антисекреторни средства" },
               new TypeOfMedication { Id=14,Name = "Антиулкусни средства" },
               new TypeOfMedication { Id=15,Name = "Антихипогликемични средства" },
               new TypeOfMedication { Id=16,Name = "Гонадоактивни средства" },
               new TypeOfMedication { Id=17,Name = "Диуретици" },
               new TypeOfMedication { Id=18,Name = "Ензимни панкреатични средства" },
               new TypeOfMedication { Id=19,Name = "Калциеви антагонисти" },
               new TypeOfMedication { Id=20,Name = "М-холинолитици" },
               new TypeOfMedication { Id=21,Name = "Муколитици" },
               new TypeOfMedication { Id=22,Name = "Невролептици (антипсихотици)" },
               new TypeOfMedication { Id=23,Name = "Неопиоидни (антипиретични) аналгетици" },
               new TypeOfMedication { Id=24,Name = "Нестероидни противовъзпалителни средства (Сох-инхибитори)" },
               new TypeOfMedication { Id=25,Name = "Орални антидиабетични средства" },
               new TypeOfMedication { Id=26,Name = "Очистителни (лаксативни) средства" },
               new TypeOfMedication { Id=27,Name = "Противовирусни средства" },
               new TypeOfMedication { Id=28,Name = "Средства, повлияващи растежния хормон" },
               new TypeOfMedication { Id=29,Name = "Средства, прилагани при суха кашлица" },
               new TypeOfMedication { Id=30,Name = "Тиреоактивни средства" },
               new TypeOfMedication { Id=31,Name = "Хепатопротектори" },
               new TypeOfMedication { Id=32,Name = "Холеретични (жлъчетворни) и холекинетични (жлъчегонни) средства" },
               new TypeOfMedication { Id=33,Name = "Холиномиметици" },
               new TypeOfMedication { Id=34, Name = "Хормонални контрацептиви" }
               );
            builder.Entity<ConsistencyOfMedication>().HasData(
                new ConsistencyOfMedication { Id = 1, Name = "таблетки" },
                new ConsistencyOfMedication { Id=2,Name = "капсули" },
                new ConsistencyOfMedication { Id=3,Name = "прах" },
                new ConsistencyOfMedication { Id=4,Name = "гранули" },
                new ConsistencyOfMedication { Id=5,Name = "разтвор" },
                new ConsistencyOfMedication { Id=6,Name = "емулсия" },
                new ConsistencyOfMedication { Id=7,Name = "суспензия" },
                new ConsistencyOfMedication { Id=8,Name = "сироп" },
                new ConsistencyOfMedication { Id=9,Name = "капки" },
                new ConsistencyOfMedication { Id=10,Name = "запарка, отвара" },
                new ConsistencyOfMedication { Id=11,Name = "тинктура" },
                new ConsistencyOfMedication { Id=12,Name = "инжекционни форми" },
                new ConsistencyOfMedication { Id=13,Name = "крем" },
                new ConsistencyOfMedication { Id=14,Name = "гел, желе" },
                new ConsistencyOfMedication { Id=15,Name = "паста" },
                new ConsistencyOfMedication { Id=16,Name = "маз, мехлем" },
                new ConsistencyOfMedication { Id=17,Name = "свещичка" },
                new ConsistencyOfMedication { Id=18,Name = "пяна" },
                new ConsistencyOfMedication { Id=19,Name = "пластир, трансдермални терапевтични системи" },
                new ConsistencyOfMedication { Id = 20, Name = "спрей" }
                );
        }
    }
}
