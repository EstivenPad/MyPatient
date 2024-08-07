﻿using Microsoft.EntityFrameworkCore;
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
            var solutions = entity.Solutions.Where(s => s.IsDeleted == false);

            _dbContext.Set<MedicalOrderDetail>().AddRange(solutions);
            
            return base.Create(entity);
        }

        public override Task Update(MedicalOrder entity)
        {
            var solutions = entity.Solutions.Where(s => s.IsDeleted == true);
            
            _dbContext.MedicalOrderDetails.RemoveRange(solutions);

            entity.Solutions.RemoveAll(s => s.IsDeleted == true);

            return base.Update(entity);
        }

        public override Task Delete(MedicalOrder entity)
        {
            return base.Delete(entity);
        }
    }
}
