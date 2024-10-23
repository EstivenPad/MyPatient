using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.Models;
using MyPatient.Models.ViewModels.SurgicalProcedureVM;
using System.Linq.Expressions;

namespace MyPatient.Application.Services.SurgicalProcedureServices
{
    public class SurgicalProcedureService : ISurgicalProcedureService
    {
        private readonly ISurgicalProcedureRepository _surgicalProcedureRepository;

        public SurgicalProcedureService(ISurgicalProcedureRepository surgicalProcedureRepository)
        {
            _surgicalProcedureRepository = surgicalProcedureRepository;
        }

        public async Task AddSurgicalProcedure(SurgicalProcedure surgicalProcedure)
        {
            await _surgicalProcedureRepository.Create(surgicalProcedure);
        }

        public IQueryable<SurgicalProcedure> GetAllSurgicalProcedures(Expression<Func<SurgicalProcedure, bool>>? filter, string? includeProperties = "")
        {
            return _surgicalProcedureRepository.GetAll(filter, includeProperties);
        }

        public async Task<SurgicalProcedure> GetSurgicalProcedure(Expression<Func<SurgicalProcedure, bool>> filter, string? includeProperties = null, bool? asNoTracking = true)
        {
            return await _surgicalProcedureRepository.Get(filter, includeProperties, asNoTracking);
        }

        public async Task<List<SurgicalProcedureSummary>> GetSurgicalProceduresReport(long doctorId, DateOnly fromDate, DateOnly toDate)
        {
            return await _surgicalProcedureRepository.GetReportData(doctorId, fromDate, toDate);
        }

        public async Task RemoveSurgicalProcedure(SurgicalProcedure surgicalProcedure)
        {
            await _surgicalProcedureRepository.Delete(surgicalProcedure);
        }

        public async Task UpdateSurgicalProcedure(SurgicalProcedure surgicalProcedure)
        {
            await _surgicalProcedureRepository.Update(surgicalProcedure);
        }
    }
}
