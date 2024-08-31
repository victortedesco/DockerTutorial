using Users.Domain.Models;

namespace Users.Domain.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<IEnumerable<User>> GetByName(string name);
    Task<User?> GetByEmail(string email);
    Task<User?> GetByDocument(string document);
}
