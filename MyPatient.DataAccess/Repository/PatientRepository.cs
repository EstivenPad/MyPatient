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

        public async Task<List<Patient>> GetAllFiltered(Expression<Func<Patient, bool>> expression)
        {
            return await _context.Patients
                        .Where(expression)
                        .AsNoTracking()
                        .ToListAsync();
        }
    }
}
