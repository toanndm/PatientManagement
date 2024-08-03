using PatientManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Application.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(Guid id);
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate);
    }
}
