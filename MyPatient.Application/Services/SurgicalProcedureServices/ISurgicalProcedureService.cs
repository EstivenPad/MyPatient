using MyPatient.Models;
using MyPatient.Models.ViewModels.SurgicalProcedureVM;
using System.Linq.Expressions;

namespace MyPatient.Application.Services.SurgicalProcedureServices
{
    public interface ISurgicalProcedureService
    {
        Task AddSurgicalProcedure(SurgicalProcedure medicalOrder);
        IQueryable<SurgicalProcedure> GetAllSurgicalProcedures(Expression<Func<SurgicalProcedure, bool>>? filter, string? includeProperties = "");
        Task<SurgicalProcedure> GetSurgicalProcedure(Expression<Func<SurgicalProcedure, bool>> filter, string? includeProperties = null, bool? asNoTracking = false);
        Task RemoveSurgicalProcedure(SurgicalProcedure medicalOrder);
        Task UpdateSurgicalProcedure(SurgicalProcedure medicalOrder);
        Task<List<SurgicalProcedureSummary>> GetSurgicalProceduresReport(long doctorId, DateOnly fromDate, DateOnly toDate);
    }
}
