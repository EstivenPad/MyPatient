using Microsoft.AspNetCore.Mvc.Rendering;
using MyPatient.DataAccess.Repository;
using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Application.Services.MA
{
    public class MAService : IMAService
    {
        protected readonly IMARepository _maRepository;

        public MAService(IMARepository maRepository)
        {
            _maRepository = maRepository;
        }

        public async Task AddMA(Models.MA ma)
        {
            await _maRepository.Create(ma);
        }

        public async Task<IEnumerable<Models.MA>> GetAllMAs(Expression<Func<Models.MA, bool>>? filter)
        {
            return await _maRepository.GetAll(filter, string.Empty);
        }

        public async Task<Models.MA> GetMA(Expression<Func<Models.MA, bool>> filter)
        {
            return await _maRepository.Get(filter, string.Empty);
        }

        public async Task<IEnumerable<SelectListItem>> PopulateMASelect()
        {
            var MAList = await _maRepository.GetAll(x => true, string.Empty);

            MAList = MAList.OrderBy(ma => ma.FirstName);

            return MAList.Select(ma => new SelectListItem
            {
                Text = String.Concat(ma.Sex ? "Dra. " : "Dr. ", " ", ma.FirstName, " ", ma.LastName),
                Value = ma.Id.ToString()
            });
        }

        public async Task RemoveMA(Models.MA ma)
        {
            await _maRepository.Delete(ma);
        }

        public async Task UpdateMA(Models.MA ma)
        {
            await _maRepository.Update(ma);
        }
    }
}
