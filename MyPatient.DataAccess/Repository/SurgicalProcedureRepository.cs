using Microsoft.EntityFrameworkCore;
using MyPatient.DataAccess.DataContext;
using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.Models;
using MyPatient.Models.ViewModels.SurgicalProcedureVM;
using System.Linq.Expressions;

namespace MyPatient.DataAccess.Repository
{
    public class SurgicalProcedureRepository : BaseRepository<SurgicalProcedure>, ISurgicalProcedureRepository
    {
        private readonly DatabaseContext _dbContext;

        public SurgicalProcedureRepository(DatabaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<SurgicalProcedure> GetAll(Expression<Func<SurgicalProcedure, bool>>? filter = null, string? includeProperties = null, bool asNoTracking = true)
        {
            IQueryable<SurgicalProcedure> query = dbSet;

            if (asNoTracking)
                query = query.AsNoTracking();

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

            return query;
        }

        public async Task<List<SurgicalProcedureSummary>> GetReportData(long doctorId, DateOnly fromDate, DateOnly toDate)
        {
            var query = await _dbContext.Database
                                        .SqlQuery<SurgicalProcedureReportVM>(
                                            $"EXEC SP_GetSurgicalProcedureReport {doctorId}, {fromDate}, {toDate}")
                                        .AsNoTracking()
                                        .ToListAsync();

            var data = query.GroupBy(x => x.SurgicalProcedureID)
                            .Select(x => new SurgicalProcedureSummary {
                                Id = x.Key,
                                SurgicalProcedure = new SurgicalProcedureInfo {
                                                        CreatedDate = x.First().CreatedDate,
                                                        Name_Patient = x.First().Name_Patient,
                                                        Age_Patient = x.First().Age_Patient,
                                                        Diagnostic = x.First().Diagnostic,
                                                        Procedure = x.First().Procedure,
                                                        Name_MA = x.First().Name_MA,
                                                        Level_MA = x.First().Level_MA },
                                Discoveries = x.Select(x => new DiscoveryInfo {
                                                    ID = x.ID_Discovery,
                                                    Description = x.Discovery
                                                }).DistinctBy(h => h.ID).ToList(),
                                Residents = x.Select(x => new ResidentInfo {
                                    ID = x.ID_Resident,
                                    Name = x.Name_Resident,
                                    Level = x.Level_Resident
                                }).DistinctBy(r => r.ID).ToList()
                            }).ToList();

            return data;
        }

        public override async Task Update(SurgicalProcedure updatedSurgicalProcedure)
        {
            var surgicalProcedureFromDB = await _dbContext.SurgicalProcedures
                .Include(x => x.Discoveries)
                .Include(x => x.DoctorSurgicalProcedures)
                .FirstOrDefaultAsync(x => x.Id == updatedSurgicalProcedure.Id);

            surgicalProcedureFromDB.Procedure = updatedSurgicalProcedure.Procedure;
            surgicalProcedureFromDB.Diagnostic = updatedSurgicalProcedure.Diagnostic;

            // ************************* Handling Many-to-Many: Doctor_SurgicalProcedure *************************

            var doctorIDsFromDB = surgicalProcedureFromDB.DoctorSurgicalProcedures.Select(dsp => dsp.DoctorId).ToList();
            var currentDoctorsID = updatedSurgicalProcedure.DoctorSurgicalProcedures.Select(d => d.DoctorId).ToList();

            var doctorsToAdd = updatedSurgicalProcedure.DoctorSurgicalProcedures.ExceptBy(doctorIDsFromDB, x => x.DoctorId).ToList();
            var doctorsToRemove = doctorIDsFromDB.Except(currentDoctorsID).ToList();

            surgicalProcedureFromDB.DoctorSurgicalProcedures.RemoveAll(sc => doctorsToRemove.Contains(sc.DoctorId));

            foreach (var doctorId in doctorsToAdd)
            {
                surgicalProcedureFromDB.DoctorSurgicalProcedures.Add(
                    new Doctor_SurgicalProcedure
                    {
                        DoctorId = doctorId.DoctorId,
                        SurgicalProdecureId = updatedSurgicalProcedure.Id
                    }
                );
            }

            // ************************* Handling Many-to-Many: Doctor_SurgicalProcedure *************************

            var discoveryIDsFromDB = surgicalProcedureFromDB.Discoveries.Select(d => d.Id).ToList();
            var currentDiscoveriesID = updatedSurgicalProcedure.Discoveries.Select(d => d.Id).ToList();

            var discoveriesToRemove = discoveryIDsFromDB.Except(currentDiscoveriesID).ToList();

            surgicalProcedureFromDB.Discoveries.RemoveAll(d => discoveriesToRemove.Contains(d.Id));

            foreach (var discovery in updatedSurgicalProcedure.Discoveries)
            {
                var existingDiscovery = surgicalProcedureFromDB.Discoveries
                    .FirstOrDefault(d => d.Id == discovery.Id);

                // If it exists - update it
                if (existingDiscovery != null)
                {
                    existingDiscovery.Description = discovery.Description;
                }
                else
                {
                    // If it doesn't exist - add it
                    surgicalProcedureFromDB.Discoveries.Add(new SurgicalProcedureDiscoveries
                    {
                        Description = discovery.Description,
                        SurgicalProcedureId = updatedSurgicalProcedure.Id
                    });
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        
    }
}