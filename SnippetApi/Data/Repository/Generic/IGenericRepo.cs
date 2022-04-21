using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;

namespace SnippetApi.Data.Repository.Generic
{
    public interface IGenericRepo <TEntity> where TEntity : class
    {
        Task AddAsync([NotNull] TEntity entity);
        ValueTask<TEntity> FindAsync(object[] keyValues);
        EntityEntry<TEntity> Remove([NotNull] TEntity entity);
        void Update([NotNull] TEntity entity);
    }
}
