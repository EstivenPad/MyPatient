using Microsoft.EntityFrameworkCore;
using MyPatient.Models;
using MyPatient.Models.Enums;

namespace MyPatient.DataAccess.DataContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options){}

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalOrder> MedicalOrders { get; set; }
        public DbSet<MedicalOrderDetail> MedicalOrderDetails { get; set; }
        public DbSet<SurgicalProcedure> SurgicalProcedures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.MA)
                .WithMany()
                .HasForeignKey(p => p.MAId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalOrder>()
                .HasOne(mo => mo.Patient)
                .WithMany()
                .HasForeignKey(mo => mo.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalOrder>()
                .HasOne(mo => mo.MA)
                .WithMany()
                .HasForeignKey(mo => mo.MAId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalOrder>()
                .HasMany(mo => mo.Solutions)
                .WithOne(mod => mod.MedicalOrder)
                .HasForeignKey(mod => mod.MedicalOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Doctor_SurgicalProcedure>()
                        .HasKey(ds => new { ds.DoctorId, ds.SurgicalProdecureId });

            modelBuilder.Entity<Doctor_SurgicalProcedure>()
                .HasOne(ds => ds.Doctor)
                .WithMany(s => s.DoctorSurgicalProcedures)
                .HasForeignKey(ds => ds.DoctorId);

            modelBuilder.Entity<Doctor_SurgicalProcedure>()
                .HasOne(ds => ds.SurgicalProcedure)
                .WithMany(d => d.DoctorSurgicalProcedures)
                .HasForeignKey(ds => ds.SurgicalProdecureId);


            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = 1,
                    Type = TypeDoctor.MA,
                    FirstName = "Miguel",
                    LastName = "Tejada",
                    Sex = false,
                    Identification = "402-1234567-0",
                    Exequatur = "1536-23"
                });
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    Record = "1234",
                    Name = "Guillermo Reyes",
                    Weight = 145.3,
                    Age = 25,
                    Identification = "402-1234567-1",
                    Sex = false,
                    IsInsured = true,
                    ARS = "SeNaSa",
                    NSS = "1234",
                    MAId = 1
                });
        }
    }
}
