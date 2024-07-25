using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Application.Services.PatientServices
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        
        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task AddPatient(Patient patient)
        {
            await _patientRepository.Create(patient);
        }

        public IQueryable<Patient> GetAllPatients(Expression<Func<Patient, bool>>? filter, string? includeProperties = null)
        {
            return _patientRepository.GetAll(filter, includeProperties);
        }

        public async Task<Patient> GetPatient(Expression<Func<Patient, bool>> filter, string? includeProperties = null)
        {
            return await _patientRepository.Get(filter, includeProperties);
        }

        public async Task RemovePatient(Patient patient)
        {
            await _patientRepository.Delete(patient);
        }

        public async Task UpdatePatient(Patient patient)
        {
            await _patientRepository.Update(patient);
        }
    }
}
