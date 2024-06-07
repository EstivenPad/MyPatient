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
    }
}
