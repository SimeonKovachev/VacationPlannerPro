using System.Linq.Expressions;

namespace VacationPlannerPro.Data.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        ValueTask<T?> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task DeleteAllAsync();

        Task<(IEnumerable<T> data, int totalCount)> GetPaginatedWithIncludeAsync<TProperty>(
            int pageNumber,
            int pageSize,
            Expression<Func<T, bool>>? predicate = null,
            Expression<Func<T, object>>? orderBy = null,
            Expression<Func<T, TProperty>>? includeProperty = null);

        Task<T?> GetEntityWithNavigationPropertyByIdAsync<T, TNavigation>(Guid id, Expression<Func<T, TNavigation>> navigationProperty)
            where T : class;
    }
}
