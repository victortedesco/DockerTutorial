using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Domain.Repositories;
using Users.Infrastructure.Context;
using Users.Infrastructure.Repositories;
using Users.Infrastructure.Services;
using Users.Infrastructure.Services.Interfaces;

namespace Users.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbPassword = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");

        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new ArgumentException("The DefaultConnection string was not found.");

        connectionString = connectionString.Replace("${MSSQL_SA_PASSWORD}", dbPassword);

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
