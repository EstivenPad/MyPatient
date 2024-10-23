using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyPatient.Application.Services.DoctorServices;
using MyPatient.Application.Services.MedicalOrderServices;
using MyPatient.Application.Services.PatientServices;
using MyPatient.Application.Services.SurgicalProcedureServices;
using System.Reflection;

namespace MyPatient.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IMedicalOrderService, MedicalOrderService>();
            services.AddScoped<ISurgicalProcedureService, SurgicalProcedureService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
