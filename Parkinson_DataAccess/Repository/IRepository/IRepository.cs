using System.Linq.Expressions;

namespace Parkinson_DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        Task CreateAsync(T entity);
        Task<IEnumerable<T>> CreateListAsync(IEnumerable<T> entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();

    }
}
