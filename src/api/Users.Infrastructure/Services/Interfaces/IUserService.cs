using Users.Infrastructure.DTO;

namespace Users.Infrastructure.Services.Interfaces;

public interface IUserService : IService<UserDTO, Guid>
{
    Task<IEnumerable<UserDTO>> GetByName(string name);
    Task<UserDTO?> GetByEmail(string email);
    Task<UserDTO?> GetByDocument(string document);
}
