using Microsoft.EntityFrameworkCore;
using Parkinson_DataAccess.Data;
using Parkinson_DataAccess.Repository.IRepository;
using System.Linq.Expressions;

namespace Parkinson_DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private protected readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter is not null)
                query = query.Where(filter);
            return query.ToList();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter is not null)
                query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<IEnumerable<T>> CreateListAsync(IEnumerable<T> entity)
        {
            await dbSet.AddRangeAsync(entity);
            await SaveAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
