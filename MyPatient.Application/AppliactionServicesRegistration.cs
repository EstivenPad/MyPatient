using Microsoft.Extensions.DependencyInjection;
using MyPatient.Application.Services.MAServices;
using MyPatient.Application.Services.MedicalOrderServices;
using MyPatient.Application.Services.PatientServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IMAService, MAService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IMedicalOrderService, MedicalOrderService>();

            return services;
        }
    }
}
