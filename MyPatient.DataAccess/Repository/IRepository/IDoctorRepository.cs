using MyPatient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.DataAccess.Repository.IRepository
{
    public interface IDoctorRepository : IBaseRepository<Doctor>
    {
        Task<bool> HasAnyPatient(long doctorId);
    }
}
