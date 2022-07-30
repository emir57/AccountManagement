using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IAsyncEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task<bool> AddAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                EntityEntry entityEntry = context.Entry(entity);
                entityEntry.State = EntityState.Added;
                int row = await context.SaveChangesAsync();
                return row > 0;
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                EntityEntry entityEntry = context.Entry(entity);
                entityEntry.State = EntityState.Deleted;
                //int row = await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().AsNoTracking().ToListAsync();
            }
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(predicate);
            }
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().FindAsync(id);
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                EntityEntry entityEntry = context.Entry(entity);
                entityEntry.State = EntityState.Modified;
                int row = await context.SaveChangesAsync();
                return row > 0;
            }
        }
    }
}
