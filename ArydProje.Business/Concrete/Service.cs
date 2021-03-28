using ArydProje.Core.Abstract.Repositories;
using ArydProje.Core.Abstract.Results;
using ArydProje.Core.Abstract.Services;
using ArydProje.Core.Abstract.UnitOfWorks;
using ArydProje.Core.Concrete.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Business.Concrete
{
    public class Service<T> : IService<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<T> _repository;

        public Service(IUnitOfWork unitOfWork, IRepository<T> repository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IDataResult<bool>> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate is null)
                return new DataResult<bool>(Status.Error);
            else
            {
                var result = await _repository.AnyAsync(predicate);
                return new DataResult<bool>(Status.Success, result);
            }
        }

        public async Task<IDataResult<IEnumerable<T>>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();

            if (entities == null)
                return new DataResult<IEnumerable<T>>(Status.Error);
            return new DataResult<IEnumerable<T>>(Status.Success, entities);
        }

        public async Task<IDataResult<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            if (predicate is null)
                return new DataResult<T>(Status.Error);
            else
            {
                var result = await _repository.GetAsync(predicate);
                if (result == null)
                    return new DataResult<T>(Status.Error);
                return new DataResult<T>(Status.Success, result);
            }
        }

        public async Task<IResult> UpdateAsync(T entity)
        {
            if (entity == null)
                return new Result(Status.Error);
            else
            {
                await _repository.UpdateAsync(entity);
                await _unitOfWork.SaveAsync();

                return new Result(Status.Success);
            }
        }
    }
}
