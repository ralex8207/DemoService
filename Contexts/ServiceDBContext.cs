using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DemoService.Entities;

namespace DemoService.Contexts {

    public class ServiceDBContext : DbContext {

        public ServiceDBContext(DbContextOptions<ServiceDBContext> options) : base(options) { }

        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseLoggerFactory(EFLoggerFactory);

        public static readonly ILoggerFactory EFLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
    }
}