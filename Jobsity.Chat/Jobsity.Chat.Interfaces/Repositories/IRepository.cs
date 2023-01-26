using Jobsity.Chat.Core.Interfaces;
using Jobsity.Chat.Models;

namespace Jobsity.Chat.Interfaces.Repositories
{
    public interface IRepository<TEntity, out TPredicateReturnType> 
        where TEntity : JobsityModel
        where TPredicateReturnType : IRepository<TEntity, TPredicateReturnType>
    {
        TPredicateReturnType Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        TPredicateReturnType Include<TProperty>(System.Linq.Expressions.Expression<Func<TEntity, TProperty>> member) where TProperty : class;
        TPredicateReturnType OrderBy(System.Linq.Expressions.Expression<Func<TEntity, object>> member);
        TPredicateReturnType OrderByDescending(System.Linq.Expressions.Expression<Func<TEntity, object>> member);

        Task<TEntity?> FindByIdAsync(Guid id);
        Task<IEnumerable<TEntity>?> GetAsync();
        Task<IPageableResponse<TEntity>?> GetPaginatedAsync(int page, int pageSize);
        Task<TEntity?> AddAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(Guid id, TEntity entity);
        Task<TEntity?> DeleteAsync(Guid id);
    }
}
