namespace Users.Domain.Models;

public class User(string name, string email, string document) : IEntity<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public string Document { get; private set; } = document;

    public void Update(User request)
    {
        Name = request.Name;
        Email = request.Email.ToLower();
    }
}
