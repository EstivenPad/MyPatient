using Microsoft.EntityFrameworkCore;
using MyPatient.DataAccess.DataContext;
using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.DataAccess.Repository
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<bool> HasAnyPatient(long doctorId)
        {
            return await _dbContext.Patients.AnyAsync(p => p.MAId == doctorId) == true;
        }
    }
}
