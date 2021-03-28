using ArydProje.Core.Abstract.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Core.Abstract.Services
{
    public interface IService<T> where T : class
    {
        Task<IDataResult<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<IDataResult<IEnumerable<T>>> GetAllAsync();
        Task<IDataResult<bool>> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<IResult> UpdateAsync(T entity);
    }
}
