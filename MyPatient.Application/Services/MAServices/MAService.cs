using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPatient.DataAccess.Repository;
using MyPatient.DataAccess.Repository.IRepository;
using MyPatient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Application.Services.MAServices
{
    public class MAService : IMAService
    {
        private readonly IMARepository _maRepository;

        public MAService(IMARepository maRepository)
        {
            _maRepository = maRepository;
        }

        public async Task AddMA(MA ma)
        {
            await _maRepository.Create(ma);
        }

        public IQueryable<MA> GetAllMAs(Expression<Func<MA, bool>>? filter)
        {
            return _maRepository.GetAll(filter, string.Empty);
        }

        public async Task<MA> GetMA(Expression<Func<MA, bool>> filter)
        {
            return await _maRepository.Get(filter, string.Empty);
        }

        public IEnumerable<SelectListItem> PopulateMASelect()
        {
            var MAList = _maRepository.GetAll(x => true, string.Empty);

            MAList = MAList.OrderBy(ma => ma.FirstName);

            return MAList.Select(ma => new SelectListItem
            {
                Text = String.Concat(ma.Sex ? "Dra. " : "Dr. ", ma.FirstName, " ", ma.LastName),
                Value = ma.Id.ToString()
            });

        }

        public async Task RemoveMA(MA ma)
        {
            await _maRepository.Delete(ma);
        }

        public async Task UpdateMA(MA ma)
        {
            await _maRepository.Update(ma);
        }
    }
}
