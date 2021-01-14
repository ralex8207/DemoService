using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DemoService.Contexts;
using DemoService.Ef.UOF;

namespace DemoService.Extensions {

    public static class ServiceCollectionExtensions {

        public static void AddUnitOfWork<TContext>(this IServiceCollection services) where TContext : DbContext {

            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();
        }

        public static void MigrateDatabase(this IServiceProvider provider) {

            using var scope   = provider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ServiceDBContext>();

            try {
                context.Database.Migrate();
            }
            catch (Exception ex) {
                throw;
            }
        }

    }
}