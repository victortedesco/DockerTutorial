using Users.Domain.Models;
using Users.Infrastructure.DTO;

namespace Users.Infrastructure.Extensions;

public static class UserDTOExtensions
{
    public static UserDTO ToDTO(this User user)
    {
        return new(user.Id, user.Name, user.Email, user.Document);
    }

    public static IEnumerable<UserDTO> ToDTO(this IEnumerable<User> users)
    {
        return users.Select(ToDTO);
    }
}
