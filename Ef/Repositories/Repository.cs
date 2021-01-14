using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DemoService.Ef.Repositories {

    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : struct {

        /// <summary>
        ///
        /// </summary>
        /// <param name="dbContext"></param>
        public Repository(DbContext dbContext) {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet     = dbContext.Set<TEntity>();
        }

        private DbSet<TEntity> DbSet { get; }

        public DbContext DbContext { get; }

        /// <summary>
        /// Чтение данных
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Read() =>
            DbSet.AsNoTracking().AsQueryable();

        public IQueryable<TEntity> Read(Expression<Func<TEntity, bool>> predicate) =>
            Read().Where(predicate);

        public ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default) {
            DbContext.Entry(entity).State = EntityState.Added;
            return DbSet.AddAsync(entity, cancellationToken);
        }

        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) {
            DbContext.Entry(entity).State = EntityState.Modified;
            DbSet.Update(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default) {
            DbContext.Entry(entity).State = EntityState.Deleted;
            DbSet.Remove(entity);

            return Task.CompletedTask;
        }
    }

}