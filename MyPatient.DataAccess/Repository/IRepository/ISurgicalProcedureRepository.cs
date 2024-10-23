using MyPatient.Models;
using MyPatient.Models.ViewModels.SurgicalProcedureVM;
using System.Linq.Expressions;

namespace MyPatient.DataAccess.Repository.IRepository
{
    public interface ISurgicalProcedureRepository : IBaseRepository<SurgicalProcedure>
    {
        IQueryable<SurgicalProcedure> GetAll(Expression<Func<SurgicalProcedure, bool>>? filter = null, string? includeProperties = null);
        Task<List<SurgicalProcedureSummary>> GetReportData(long doctorId, DateOnly fromDate, DateOnly toDate);
    }
}
