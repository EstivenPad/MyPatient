using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.DataAccess.Repository.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);

    }
}
