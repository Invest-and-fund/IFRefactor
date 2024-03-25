using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AcmeStudios.ApiRefactor.Data;
using AcmeStudios.ApiRefactor.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcmeStudios.ApiRefactor.Repositories;

public abstract class EfCoreRepository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : class, IEntity
    where TContext : EFStudioDbContext
{
    private readonly TContext context;
    public EfCoreRepository(TContext context)
    {
        this.context = context;
    }
    public async Task<TEntity> Add(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> Delete(long id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            return entity;
        }

        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> Get(long id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return entity;
    }
    public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = context.Set<TEntity>().Where(expression);

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync();
    }
    
    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,  params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        
        return await query.FirstOrDefaultAsync(predicate);
    }
    public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        
        return await query.FirstAsync(predicate);
    }


    public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }
    public async Task<List<TResult>> Select<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, object>>[] includes = null,
        Expression<Func<TEntity, object>> orderBy = null)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (orderBy != null)
        {
            query = query.OrderBy(orderBy);
        }

        return await query.Select(selector).ToListAsync();
    }



    public async Task AddAsync(TEntity item)
    {
        await context.Set<TEntity>().AddAsync(item);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}