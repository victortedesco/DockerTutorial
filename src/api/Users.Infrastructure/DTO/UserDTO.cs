namespace Users.Infrastructure.DTO;

public record UserDTO(Guid Id, string Name, string Email, string Document) : IDTO<Guid>;
