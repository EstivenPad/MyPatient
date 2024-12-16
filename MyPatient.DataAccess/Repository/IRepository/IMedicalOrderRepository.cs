using MyPatient.Models;
using MyPatient.Models.ViewModels.MedicalOrderVM;
using MyPatient.Web.Models.Enums;

namespace MyPatient.DataAccess.Repository.IRepository
{
    public interface IMedicalOrderRepository : IBaseRepository<MedicalOrder>
    {
        Task<MedicalOrder> GetLast(long patientId);
        Task<MedicalOrderSummary> GetReportData(long medicalOrderId, TypeMedicalOrder type);
    }
}
