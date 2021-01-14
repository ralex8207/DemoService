using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DemoService.Ef.Repositories.Users {

    public interface IUsersRepository {

        Task AddAsync(Entities.Users user, CancellationToken cancellationToken = default);
        Task UpdateAsync(Entities.Users user, CancellationToken cancellationToken = default);
        Task DeleteAsync(Entities.Users user, CancellationToken cancellationToken = default);
        Task<List<Entities.Users>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Entities.Users> GetAsync(int id, CancellationToken cancellationToken = default);
    }
}