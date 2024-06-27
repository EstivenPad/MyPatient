using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Application.Services.MA
{
    public interface IMAService
    {
        Task AddMA(Models.MA ma);
        Task<IEnumerable<Models.MA>> GetAllMAs(Expression<Func<Models.MA, bool>>? filter);
        Task<Models.MA> GetMA(Expression<Func<Models.MA, bool>> filter);
        Task<IEnumerable<SelectListItem>> PopulateMASelect();
        Task RemoveMA(Models.MA ma);
        Task UpdateMA(Models.MA ma);
    }
}
