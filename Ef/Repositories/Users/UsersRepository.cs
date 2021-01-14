using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DemoService.Contexts;

namespace DemoService.Ef.Repositories.Users {
    public class UsersRepository : Repository<Entities.Users, int>, IUsersRepository {

        public UsersRepository(ServiceDBContext dbContext) : base(dbContext) { }

        public new Task AddAsync(Entities.Users user, CancellationToken cancellationToken = default) {
            base.AddAsync(user, cancellationToken);
            return Task.CompletedTask;
        }

        public new Task UpdateAsync(Entities.Users user, CancellationToken cancellationToken = default) =>
            base.UpdateAsync(user, cancellationToken);

        public new Task DeleteAsync(Entities.Users users, CancellationToken cancellationToken = default) =>
            base.DeleteAsync(users, cancellationToken);

        public Task<List<Entities.Users>> GetAllAsync(CancellationToken cancellationToken = default) =>
            Read().ToListAsync(cancellationToken);

        public Task<Entities.Users> GetAsync(int id, CancellationToken cancellationToken = default) =>
            Read(item => item.Id == id).SingleOrDefaultAsync(cancellationToken);
    }
}