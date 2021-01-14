using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DemoService.Contexts;
using DemoService.Ef.Repositories.Users;
using DemoService.Extensions;

namespace DemoService {
    public class Startup {

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddScoped<IUsersRepository, UsersRepository>();

            services.AddDbContext<ServiceDBContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("DBConnection")))
                .AddUnitOfWork<ServiceDBContext>();

            services.AddSwaggerGenUI("Demo service", AppContext.BaseDirectory);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, ILogger<Program> logger) {

            app.ApplicationServices.MigrateDatabase();
            app.Use(async (context, next) => {
                logger.LogInformation("REQUEST: Time:[{0}] => Method:[{1}], Host:[{2}], Path:[{3}]",
                    DateTime.UtcNow,
                    context.Request.Method,
                    context.Request.Host.Host,
                    context.Request.Path);

                await next();
            });


            app.UseRouting();
            app.UseSwaggerUI("/swagger/v1/swagger.json", "Demo service");
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}