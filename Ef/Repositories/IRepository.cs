using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DemoService.Ef.Repositories {

    public interface IRepository<TEntity, in TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : struct {

        DbContext DbContext { get; }

        IQueryable<TEntity> Read();

        IQueryable<TEntity> Read(Expression<Func<TEntity, bool>> predicate);

        ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}