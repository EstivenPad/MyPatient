using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyPatient.Models;

namespace MyPatient.Application.Services.DoctorServices
{
    public interface IDoctorService
    {
        Task AddDoctor(Doctor doctor);
        IQueryable<Doctor> GetAllDoctors(Expression<Func<Doctor, bool>>? filter);
        Task<Doctor> GetDoctor(Expression<Func<Doctor, bool>> filter);
        Task<bool> HasPatients(int doctorId);
        IEnumerable<SelectListItem> PopulateMADroplist();
        Task RemoveDoctor(Doctor doctor);
        Task UpdateDoctor(Doctor doctor);
    }
}
