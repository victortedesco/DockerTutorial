using Users.API.ViewModels;
using Users.Infrastructure.DTO;

namespace Users.API.Extensions;

public static class UserViewModelExtensions
{
    public static IEnumerable<UserViewModel> ToViewModel(this IEnumerable<UserDTO> users)
    {
        return users.Select(ToViewModel);
    }

    public static UserViewModel ToViewModel(this UserDTO user)
    {
        return new(user.Id, user.Name, user.Email, user.Document);
    }
}
