using MyPatient.DataAccess.Repository;
using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
