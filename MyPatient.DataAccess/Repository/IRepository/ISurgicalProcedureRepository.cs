using MyPatient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.DataAccess.Repository.IRepository
{
    public interface ISurgicalProcedureRepository : IBaseRepository<SurgicalProcedure>
    {
        IQueryable<SurgicalProcedure> GetAll(Expression<Func<SurgicalProcedure, bool>>? filter = null, string? includeProperties = null);
    }
}
