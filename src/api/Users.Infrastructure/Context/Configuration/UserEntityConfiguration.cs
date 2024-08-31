using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Models;

namespace Users.Infrastructure.Context.Configuration;

internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.Property(u => u.Name).IsRequired().HasMaxLength(32);

        builder.Property(u => u.Email).IsRequired().HasMaxLength(50);

        builder.Property(u => u.Document).IsRequired().HasMaxLength(14);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasIndex(u => u.Document).IsUnique();
    }
}
