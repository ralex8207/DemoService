using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DemoService.Ef.UOF {

    public interface IUnitOfWork<out TContext> : IUnitOfWork where TContext : DbContext {

        TContext DbContext { get; }
    }

    public interface IUnitOfWork : IDisposable {

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }

}