using Microsoft.EntityFrameworkCore;
using MyPatient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.DataAccess.Repository.IRepository
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<bool> HasAnyMedicalOrder(long patientId);
    }
}
