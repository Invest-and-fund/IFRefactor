using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AcmeStudios.ApiRefactor.Contracts
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T item);
        void Update(T item);
        void Delete(T item);
        Task SaveChangesAsync();
    }
}
