using Microsoft.EntityFrameworkCore;
using MyPatient.DataAccess.DataContext;
using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.Models;
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