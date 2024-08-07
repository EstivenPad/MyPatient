using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyPatient.Models;

namespace MyPatient.Application.Services.MAServices
{
    public interface IMAService
    {
        Task AddMA(MA ma);
        IQueryable<MA> GetAllMAs(Expression<Func<MA, bool>>? filter);
        Task<MA> GetMA(Expression<Func<MA, bool>> filter);
        IEnumerable<SelectListItem> PopulateMASelect();
        Task RemoveMA(MA ma);
        Task UpdateMA(MA ma);
    }
}
