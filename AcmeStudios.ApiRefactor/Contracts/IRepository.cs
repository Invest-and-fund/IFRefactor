using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AcmeStudios.ApiRefactor.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
        Task AddAsync(TEntity item);
        void Update(TEntity item);
        void Delete(TEntity item);
        Task SaveChangesAsync();
    }
}
