using Microsoft.AspNetCore.Mvc.Rendering;
using MyPatient.Models;
using MyPatient.Web.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Application.Services.MedicalOrderServices
{
    public interface IMedicalOrderService
    {
        Task AddMedicalOrder(MedicalOrder medicalOrder);
        IQueryable<MedicalOrder> GetAllMedicalOrders(Expression<Func<MedicalOrder, bool>>? filter);
        Task<MedicalOrder> GetLastMedicalOrder(TypeMedicalOrder type, long patientId);
        Task<MedicalOrder> GetMedicalOrder(Expression<Func<MedicalOrder, bool>> filter, string? includeProperties = null);
        Task RemoveMedicalOrder(MedicalOrder medicalOrder);
        Task UpdateMedicalOrder(MedicalOrder medicalOrder);
    }
}
