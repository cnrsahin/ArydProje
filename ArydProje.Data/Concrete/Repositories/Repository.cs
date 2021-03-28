using ArydProje.Core.Abstract.Repositories;
using ArydProje.Data.Concrete.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArydProje.Data.Concrete.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(OrderDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public T Add(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await (predicate == null ? _dbSet.CountAsync() : _dbSet.CountAsync(predicate));
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() =>
            {
                _dbSet.Remove(entity);
            });
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryResult = _dbSet;

            if (predicate != null)
            {
                queryResult = queryResult.Where(predicate).AsNoTracking();
            }

            if (includeProperties.Any())
            {
                foreach (var item in includeProperties)
                {
                    queryResult = queryResult.Include(item);
                }
            }

            return await queryResult.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryResult = _dbSet;
            queryResult = queryResult.Where(predicate).AsNoTracking();

            if (includeProperties.Any())
            {
                foreach (var item in includeProperties)
                {
                    queryResult = queryResult.Include(item);
                }
            }

            return await queryResult.SingleOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() =>
            {
                _dbSet.Update(entity);
            });
        }
    }
}
