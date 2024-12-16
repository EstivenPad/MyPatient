using Microsoft.EntityFrameworkCore;
using MyPatient.DataAccess.DataContext;
using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.Models;
using MyPatient.Models.ViewModels.MedicalOrderVM;
using MyPatient.Web.Models.Enums;
using System.Linq.Expressions;
using System.Linq;

namespace MyPatient.DataAccess.Repository
{
    public class MedicalOrderRepository : BaseRepository<MedicalOrder>, IMedicalOrderRepository
    {
        private readonly DatabaseContext _dbContext;

        public MedicalOrderRepository(DatabaseContext context) : base(context)
        {
            _dbContext = context;
        }

        public override IQueryable<MedicalOrder> GetAll(Expression<Func<MedicalOrder, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<MedicalOrder> query = dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            query = query.OrderByDescending(mo => mo.CreatedDate)
                         .ThenByDescending(mo => mo.CreatedTime);

            return query;
        }

        public async Task<MedicalOrder> GetLast(long patientId)
        {
            var lastMedicalOrder = await _dbContext.MedicalOrders
                .Where(mo => mo.PatientId == patientId)
                .Include(mo => mo.MA)
                .Include(mo => mo.Patient)
                .Include(mo => mo.Solutions)
                .OrderBy(mo => mo.Id)
                .AsNoTracking()
                .LastOrDefaultAsync();

            return lastMedicalOrder;
        }

        public async Task<MedicalOrderSummary> GetReportData(long medicalOrderId, TypeMedicalOrder type)
        {
            var query = await _dbContext.Database
                                        .SqlQuery<MedicalOrderReportVM>(
                                            $"EXEC SP_GetMedicalOrderById {medicalOrderId}, {type}")
                                        .AsNoTracking()
                                        .ToListAsync();

            var data = query.GroupBy(x => x.Id)
                            .Select(x => new MedicalOrderSummary
                            {
                                Id = x.Key,
                                MedicalOrder = new MedicalOrderInfo
                                {
                                    Type = x.First().Type,
                                    Service = x.First().Service,
                                    Room = x.First().Room,
                                    CreatedDate = x.First().CreatedDate,
                                    CreatedTime = x.First().CreatedTime,
                                    Diagnostic = x.First().Diagnostic,
                                    GeneralMeasures = x.First().GeneralMeasures,
                                    Diet = x.First().Diet,
                                    Cures = x.First().Cures,
                                    Position = x.First().Position,
                                    SpecialControls = x.First().SpecialControls,
                                    DREN = x.First().DREN,
                                    Alergies = x.First().Alergies,
                                    Enterconsult = x.First().Enterconsult,
                                    Labs = x.First().Labs,
                                    CountDays = x.First().CountDays
                                },
                                Solutions = x.Select(x => new SolutionInfo
                                {
                                    Id = x.MedicalOrderDetailId,
                                    Name = x.SolutionName,
                                    Dose = x.Dose,
                                    Frecuency = x.Frecuency,
                                    Via = x.Via,
                                }).DistinctBy(h => h.Id).ToList(),
                                Patient = new PatientInfo
                                {
                                    Id_Patient = x.First().Id_Patient,
                                    Record = x.First().Record,
                                    Name = x.First().Name_Patient,
                                    Age = x.First().Age,
                                    Weight = x.First().Weight,
                                    ARS = x.First().ARS,
                                    NSS = x.First().NSS,
                                    Sex = x.First().Sex_Patient,
                                    MA = x.First().Name_MA,
                                }
                            }).FirstOrDefault();

            return data;
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
    }
}
