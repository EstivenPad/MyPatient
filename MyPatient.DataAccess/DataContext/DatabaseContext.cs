using Microsoft.EntityFrameworkCore;
using MyPatient.Models;

namespace MyPatient.DataAccess.DataContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<MA> MAs { get; set; }
        //public DbSet<MedicalOrder> MedicalOrder { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<MA>().HasData(
                new MA
                {
                    Id = 1,
                    FirstName = "Miguel",
                    LastName = "Tejada",
                    Sex = false,
                    Identification = "402-1234567-0",
                    Exequatur = "1536-23"
                });
        }
    }
}
