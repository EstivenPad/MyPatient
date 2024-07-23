using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Application.Services.MedicalOrder
{
    public interface IMedicalOrderService
    {
        Task AddMedicalOrder(Models.MedicalOrder medicalOrder);
        Task<IEnumerable<Models.MedicalOrder>> GetAllMedicalOrders(Expression<Func<Models.MedicalOrder, bool>>? filter);
        Task<Models.MedicalOrder> GetMedicalOrder(Expression<Func<Models.MedicalOrder, bool>> filter, string? includeProperties = null);
        Task RemoveMedicalOrder(Models.MedicalOrder medicalOrder);
        Task UpdateMedicalOrder(Models.MedicalOrder medicalOrder);
    }
}
