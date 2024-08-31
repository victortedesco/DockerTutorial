using Microsoft.EntityFrameworkCore;
using Users.Domain.Models;
using Users.Domain.Repositories;
using Users.Infrastructure.Context;

namespace Users.Infrastructure.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly DbSet<User> _users = dbContext.Users;

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetById(Guid id)
    {
        return await _users.FindAsync(id);
    }

    public async Task<IEnumerable<User>> GetByName(string name)
    {
        return await _users.AsNoTracking().Where(u => u.Name.ToLower().Contains(name.ToLower())).ToListAsync();
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email.ToLower());
    }

    public async Task<User?> GetByDocument(string document)
    {
        return await _users.AsNoTracking().FirstOrDefaultAsync(u => u.Document == document);
    }

    public async Task<User?> Add(User user)
    {
        await _users.AddAsync(user);

        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<bool> Update(Guid id, User request)
    {
        var user = await _users.FindAsync(id);

        if (user is null)
            return false;
        
        user.Update(request);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        var user = await _users.FindAsync(id);

        if (user is null)
            return false;

        _users.Remove(user);

        return await _dbContext.SaveChangesAsync() > 0;
    }
}
