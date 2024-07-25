using MyPatient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Application.Services.PatientServices
{
    public interface IPatientService
    {
        Task AddPatient(Patient patient);
        IQueryable<Patient> GetAllPatients(Expression<Func<Patient, bool>>? filter, string? includeProperties = null);
        Task<Patient> GetPatient(Expression<Func<Patient, bool>> filter, string? includeProperties = null);
        Task UpdatePatient(Patient patient);
        Task RemovePatient(Patient patient);
    }
}
