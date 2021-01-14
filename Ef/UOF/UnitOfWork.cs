using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DemoService.Ef.UOF {

    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext {

        private bool _disposed;
        private readonly ILogger<UnitOfWork<TContext>> _logger;

        public UnitOfWork(TContext context, ILogger<UnitOfWork<TContext>> logger) {

            DbContext = context ?? throw new ArgumentNullException(nameof(context));
            _logger   = logger;
        }

        public TContext DbContext { get; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            try {
                return await DbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error when to save data to database");
                return 0;
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (!_disposed)
                if (disposing)
                    DbContext.Dispose();

            _disposed = true;
        }

    }

}