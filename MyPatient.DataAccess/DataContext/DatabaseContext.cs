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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    Record = "1234",
                    Name = "Jose Reyes",
                    Weight = 145.3,
                    Age = 25,
                    Identification = "402-1234567-1",
                    Sex = false,
                    IsInsured = true,
                    ARS = "SeNaSa",
                    NSS = "1234"
                });
        }
    }
}
