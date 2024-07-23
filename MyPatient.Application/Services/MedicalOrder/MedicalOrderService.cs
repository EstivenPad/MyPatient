using MyPatient.DataAccess.Repository;
using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Application.Services.MedicalOrder
{
    public class MedicalOrderService : IMedicalOrderService
    {
        private readonly IMedicalOrderRepository _medicalOrderRepository;

        public MedicalOrderService(IMedicalOrderRepository medicalOrderRepository)
        {
            _medicalOrderRepository = medicalOrderRepository;
        }

        public async Task AddMedicalOrder(Models.MedicalOrder medicalOrder)
        {
            await _medicalOrderRepository.Create(medicalOrder);
        }

        public async Task<IEnumerable<Models.MedicalOrder>> GetAllMedicalOrders(Expression<Func<Models.MedicalOrder, bool>>? filter)
        {
            return await _medicalOrderRepository.GetAll(filter, string.Empty);
        }

        public async Task<Models.MedicalOrder> GetMedicalOrder(Expression<Func<Models.MedicalOrder, bool>> filter, string? includeProperties = null)
        {
            return await _medicalOrderRepository.Get(filter, includeProperties);
        }

        public async Task RemoveMedicalOrder(Models.MedicalOrder medicalOrder)
        {
            await _medicalOrderRepository.Delete(medicalOrder);
        }

        public async Task UpdateMedicalOrder(Models.MedicalOrder medicalOrder)
        {
            await _medicalOrderRepository.Update(medicalOrder);
        }
    }
}
