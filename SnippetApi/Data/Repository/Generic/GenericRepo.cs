using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;

namespace SnippetApi.Data.Repository.Generic
{
    public abstract class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
    {
        protected DbContext DbContext { get; }

        public GenericRepo(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task AddAsync([NotNull] TEntity entity)
        {
            await DbContext.Set<TEntity>().AddAsync(entity);
        }

        public async ValueTask<TEntity> FindAsync(object[] keyValues)
        {
            return await DbContext.Set<TEntity>().FindAsync(keyValues);
        }

        public EntityEntry<TEntity> Remove([NotNull] TEntity entity)
        {
            return DbContext.Set<TEntity>().Remove(entity);
        }

        public void Update([NotNull] TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
        }
    }
}
