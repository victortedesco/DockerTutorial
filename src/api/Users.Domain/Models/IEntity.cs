namespace Users.Domain.Models;

public interface IEntity<ID>
{
    ID Id { get; }
}
