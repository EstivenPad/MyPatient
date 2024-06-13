using MyPatient.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Application.Services.Patient
{
    public class PatientService : IPatientService
    {
        protected readonly IPatientRepository _patientRepository;
        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task AddPatient(Models.Patient patient)
        {
            await _patientRepository.Create(patient);
        }

        public async Task<List<Models.Patient>> GetAllPatients()
        {
            return await _patientRepository.GetAll();
        }

        public async Task<Models.Patient> GetPatient(Expression<Func<Models.Patient, bool>> filter)
        {
            return await _patientRepository.Get(filter);
        }

        public async Task RemovePatient(Models.Patient patient)
        {
            await _patientRepository.Delete(patient);
        }

        public async Task UpdatePatient(Models.Patient patient)
        {
            await _patientRepository.Update(patient);
        }
    }
}
