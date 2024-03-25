
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace AcmeStudios.ApiRefactor.Interfaces;

public interface IRepository<T> where T : class, IEntity
{
    Task<List<T>> GetAll();
    Task<T> Get(long id);
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(long id);
    IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
    Task<T> GetByIdAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
    Task AddAsync(T item);
    Task<T> FirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

    Task<List<TResult>> Select<TResult>(
        Expression<Func<T, TResult>> selector,
        Expression<Func<T, object>>[] includes = null,
        Expression<Func<T, object>> orderBy = null);

    Task SaveChangesAsync();
}