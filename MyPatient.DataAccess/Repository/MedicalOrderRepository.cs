using Microsoft.EntityFrameworkCore;
using MyPatient.DataAccess.DataContext;
using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.Models;
using MyPatient.Web.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.DataAccess.Repository
{
    public class MedicalOrderRepository : BaseRepository<MedicalOrder>, IMedicalOrderRepository
    {
        private readonly DatabaseContext _dbContext;

        public MedicalOrderRepository(DatabaseContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<MedicalOrder> GetLast(TypeMedicalOrder type, long patientId)
        {
            var lastMedicalOrder = await _dbContext.MedicalOrders
                .Where(mo => mo.Type == type)
                .Where(mo => mo.PatientId == patientId)
                .Include(mo => mo.MA)
                .Include(mo => mo.Patient)
                .Include(mo => mo.Solutions)
                .OrderBy(mo => mo.Id)
                .AsNoTracking()
                .LastOrDefaultAsync();

            return lastMedicalOrder;
        }

        public override Task Create(MedicalOrder entity)
        {   
            return base.Create(entity);
        }

        public override async Task Update(MedicalOrder entity)
        {
            // Update the Order entity
            _dbContext.Entry(entity).State = EntityState.Modified;

            // Get the existing details from the database
            var existingDetails = _dbContext.MedicalOrderDetails
                .Where(d => d.MedicalOrderId == entity.Id)
                .AsNoTracking()
                .ToList();

            // Process each detail in the model
            foreach (var detail in entity.Solutions)
            {
                if (detail.MedicalOrderDetailId == 0)
                {
                    // New detail - add it to the database
                    detail.MedicalOrderId = entity.Id;
                    _dbContext.MedicalOrderDetails.Add(detail);
                }
                else
                {
                    // Existing detail - update it
                    var existingDetail = existingDetails.SingleOrDefault(d => d.MedicalOrderDetailId == detail.MedicalOrderDetailId);
                    if (existingDetail != null)
                    {
                        _dbContext.Entry(existingDetail).CurrentValues.SetValues(detail);
                    }
                }
            }

            // Handle deleted details
            foreach (var existingDetail in existingDetails)
            {
                if (!entity.Solutions.Any(d => d.MedicalOrderDetailId == existingDetail.MedicalOrderDetailId))
                {
                    _dbContext.MedicalOrderDetails.Remove(existingDetail);
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public override Task Delete(MedicalOrder entity)
        {
            return base.Delete(entity);
        }
    }
}
