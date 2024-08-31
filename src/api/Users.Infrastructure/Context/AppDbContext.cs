using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Users.Domain.Models;

namespace Users.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext() : base() { }

    public AppDbContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<User> Users { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
