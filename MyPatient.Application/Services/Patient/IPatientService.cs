using MyPatient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Application.Services.Patient
{
    public interface IPatientService
    {
        Task AddPatient(Models.Patient patient);
        Task<IEnumerable<Models.Patient>> GetAllPatients(Expression<Func<Models.Patient, bool>>? filter, string? includeProperties = null);
        Task<Models.Patient> GetPatient(Expression<Func<Models.Patient, bool>> filter, string? includeProperties = null);
        Task UpdatePatient(Models.Patient patient);
        Task RemovePatient(Models.Patient patient);
    }
}
