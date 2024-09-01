using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyPatient.Application.Services.DoctorServices;
using MyPatient.Application.Services.MedicalOrderServices;
using MyPatient.Application.Services.PatientServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IMedicalOrderService, MedicalOrderService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
