using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPatient.DataAccess.Repository;
using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyPatient.Models.Enums;

namespace MyPatient.Application.Services.DoctorServices
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task AddDoctor(Doctor doctor)
        {
            await _doctorRepository.Create(doctor);
        }

        public IQueryable<Doctor> GetAllDoctors(Expression<Func<Doctor, bool>>? filter)
        {
            return _doctorRepository.GetAll(filter, string.Empty);
        }

        public async Task<Doctor> GetDoctor(Expression<Func<Doctor, bool>> filter)
        {
            return await _doctorRepository.Get(filter, string.Empty);
        }

        public async Task<bool> HasPatients(int doctorId)
        {
            return await _doctorRepository.HasAnyPatient(doctorId);
        }

        public IEnumerable<SelectListItem> PopulateDoctorDroplist(TypeDoctor type)
        {
            var DoctorList = _doctorRepository.GetAll(d => d.Type == type, string.Empty);

            DoctorList = DoctorList.OrderBy(doctor => doctor.FirstName);

            return DoctorList.Select(doctor => new SelectListItem
            {
                Text = String.Concat(doctor.Sex ? "Dra. " : "Dr. ", doctor.FirstName, " ", doctor.LastName),
                Value = doctor.Id.ToString()
            });
        }

        public async Task RemoveDoctor(Doctor doctor)
        {
            await _doctorRepository.Delete(doctor);
        }

        public async Task UpdateDoctor(Doctor doctor)
        {
            await _doctorRepository.Update(doctor);
        }
    }
}
