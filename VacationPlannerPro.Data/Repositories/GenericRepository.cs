using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VacationPlannerPro.Data.Interfaces.Repositories;

namespace VacationPlannerPro.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async ValueTask<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            _dbSet.RemoveRange(_dbSet);
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<T> data, int totalCount)> GetPaginatedWithIncludeAsync<TProperty>(
             int pageNumber,
             int pageSize,
             Expression<Func<T, bool>>? predicate = null,
             Expression<Func<T, object>>? orderBy = null,
             Expression<Func<T, TProperty>>? includeProperty = null)
        {
            var query = _dbSet.AsQueryable();

            if (includeProperty != null)
            {
                query = query.Include(includeProperty);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            var totalCount = await query.CountAsync();

            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (data, totalCount);
        }

        public async Task<T?> GetEntityWithNavigationPropertyByIdAsync<T, TNavigation>(Guid id, Expression<Func<T, TNavigation>> navigationProperty)
            where T : class
        {
            return await _context.Set<T>()
                .Include(navigationProperty)
                .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }
    }
}
