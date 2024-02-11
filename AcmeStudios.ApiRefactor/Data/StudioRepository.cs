using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AcemStudios.ApiRefactor.Data;

using AcmeStudios.ApiRefactor.Contracts;

using Microsoft.EntityFrameworkCore;

namespace AcmeStudios.ApiRefactor.Data
{
    public class StudioRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly StudioDbContext _studioDbContext;

        public StudioRepository(StudioDbContext studioDbContext)
        {
            _studioDbContext = studioDbContext;
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _studioDbContext.Set<TEntity>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.AsNoTracking();
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _studioDbContext.Set<TEntity>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task AddAsync(TEntity item)
        {
            await _studioDbContext.Set<TEntity>().AddAsync(item);
        }
        public void Update(TEntity item)
        {
            _studioDbContext.Set<TEntity>().Update(item);
        }

        public void Delete(TEntity item)
        {
            _studioDbContext.Set<TEntity>().Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _studioDbContext.SaveChangesAsync();
        }
    }
}
