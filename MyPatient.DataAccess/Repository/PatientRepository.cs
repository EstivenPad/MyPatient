using Microsoft.EntityFrameworkCore;
using MyPatient.DataAccess.DataContext;
using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.DataAccess.Repository
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(DatabaseContext context) : base(context) 
        {
        }

        public async Task<bool> HasAnyMedicalOrder(long patientId)
        {
            return await _dbContext.MedicalOrders.AnyAsync(mo => mo.PatientId == patientId) == true;
        }
    }
}
