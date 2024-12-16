using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.Models;
using MyPatient.Models.ViewModels.MedicalOrderVM;
using MyPatient.Web.Models.Enums;
using System.Linq.Expressions;

namespace MyPatient.Application.Services.MedicalOrderServices
{
    public class MedicalOrderService : IMedicalOrderService
    {
        private readonly IMedicalOrderRepository _medicalOrderRepository;

        public MedicalOrderService(IMedicalOrderRepository medicalOrderRepository)
        {
            _medicalOrderRepository = medicalOrderRepository;
        }

        public async Task AddMedicalOrder(MedicalOrder medicalOrder)
        {
            await _medicalOrderRepository.Create(medicalOrder);
        }

        public IQueryable<MedicalOrder> GetAllMedicalOrders(Expression<Func<MedicalOrder, bool>>? filter, string? includeProperties = "")
        {
            return _medicalOrderRepository.GetAll(filter, includeProperties);
        }

        public async Task<MedicalOrder> GetLastMedicalOrder(long patientId)
        {
            return await _medicalOrderRepository.GetLast(patientId);
        }

        public async Task<MedicalOrder> GetMedicalOrder(Expression<Func<MedicalOrder, bool>> filter, string? includeProperties = null)
        {
            return await _medicalOrderRepository.Get(filter, includeProperties);
        }

        public async Task<MedicalOrderSummary> GetMedicalOrderReport(long medicalOrderId, TypeMedicalOrder type)
        {
            return await _medicalOrderRepository.GetReportData(medicalOrderId, type);
        }

        public async Task RemoveMedicalOrder(MedicalOrder medicalOrder)
        {
            await _medicalOrderRepository.Delete(medicalOrder);
        }

        public async Task UpdateMedicalOrder(MedicalOrder medicalOrder)
        {
            await _medicalOrderRepository.Update(medicalOrder);
        }
    }
}
