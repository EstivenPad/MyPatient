using MyPatient.DataAccess.Repository;
using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.Models;
using MyPatient.Models.ViewModels.MedicalOrderVM;
using MyPatient.Web.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<MedicalOrder> GetLastMedicalOrder(TypeMedicalOrder type, long patientId)
        {
            return await _medicalOrderRepository.GetLast(type, patientId);
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
