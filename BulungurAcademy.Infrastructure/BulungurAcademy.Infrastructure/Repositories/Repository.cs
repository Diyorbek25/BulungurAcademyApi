﻿using BulungurAcademy.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulungurAcademy.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext context;

    public Repository(AppDbContext appDbContext) =>
        this.context = appDbContext;

    public async ValueTask<TEntity> InsertAsync(TEntity entity)
    {
        var entityEntry = await context.Set<TEntity>().AddAsync(entity);
        await context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public IQueryable<TEntity> SelectAll() =>
        context.Set<TEntity>().AsNoTracking();

    public async ValueTask<TEntity> SelectByIdAsync<TKey>(TKey id) =>
       await context.Set<TEntity>().FindAsync(keyValues: id);

    public async ValueTask<TEntity> SelectByIdWithDetailsAsync(
        Expression<Func<TEntity, bool>> expression,
        string[] includes)
    {
        IQueryable<TEntity> entity = context.Set<TEntity>();


        foreach (var item in includes)
        {
            entity = entity.Include(item);
        }

        return await entity.FirstOrDefaultAsync(expression);
    }
    public IQueryable<TEntity> SelectAllWithDetailsAsync(
        Expression<Func<TEntity, bool>> expression,
        string[] includes)
    {
        IQueryable<TEntity> entity = context.Set<TEntity>();


        foreach (var item in includes)
        {
            entity = entity.Include(item);
        }

        return entity.Where(expression);
    }

    public async ValueTask<TEntity> UpdateAsync(TEntity entity)
    {
        var entityEntry = context.Update(entity);
        await context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async ValueTask<TEntity> DeleteAsync(TEntity entity)
    {
        var entityEntry = context.Remove(entity);
        await context.SaveChangesAsync();

        return entityEntry.Entity;
    }
    public async ValueTask<int> SaveChangesAsync() =>
       await context.SaveChangesAsync();
}
