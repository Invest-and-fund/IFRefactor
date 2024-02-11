using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AcemStudios.ApiRefactor.Data;

using AcmeStudios.ApiRefactor.Contracts;

using Microsoft.EntityFrameworkCore;

namespace AcmeStudios.ApiRefactor.Data
{
    public class StudioRepository<T> : IRepository<T> where T : class
    {
        private readonly StudioDbContext _studioDbContext;

        public StudioRepository(StudioDbContext studioDbContext)
        {
            _studioDbContext = studioDbContext;
        }

        public IQueryable<T> GetAll()
        {
            return _studioDbContext.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> expression)
        {
            return await _studioDbContext.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task AddAsync(T item)
        {
            await _studioDbContext.Set<T>().AddAsync(item);
        }
        public void Update(T item)
        {
            _studioDbContext.Set<T>().Update(item);
        }

        public void Delete(T item)
        {
            _studioDbContext.Set<T>().Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _studioDbContext.SaveChangesAsync();
        }
    }
}
