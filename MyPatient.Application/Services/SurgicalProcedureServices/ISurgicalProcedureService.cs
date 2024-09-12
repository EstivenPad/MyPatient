using MyPatient.Models;
using MyPatient.Web.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Application.Services.SurgicalProcedureServices
{
    public interface ISurgicalProcedureService
    {
        Task AddSurgicalProcedure(SurgicalProcedure medicalOrder);
        IQueryable<SurgicalProcedure> GetAllSurgicalProcedures(Expression<Func<SurgicalProcedure, bool>>? filter, string? includeProperties = "");
        Task<SurgicalProcedure> GetSurgicalProcedure(Expression<Func<SurgicalProcedure, bool>> filter, string? includeProperties = null, bool? asNoTracking = false);
        Task RemoveSurgicalProcedure(SurgicalProcedure medicalOrder);
        Task UpdateSurgicalProcedure(SurgicalProcedure medicalOrder);
    }
}
