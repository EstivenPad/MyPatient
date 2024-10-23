using Microsoft.Extensions.DependencyInjection;
using MyPatient.DataAccess.DataContext;
using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MyPatient.DataAccess
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IMedicalOrderRepository, MedicalOrderRepository>();
            services.AddScoped<ISurgicalProcedureRepository, SurgicalProcedureRepository>();

            return services;
        }
    }
}
