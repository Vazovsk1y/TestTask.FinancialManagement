using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;

namespace TestTask.DAL.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ConfigureId<User, UserId>();

		builder.HasIndex(u => u.Email).IsUnique();

		builder
			.HasMany(e => e.Roles)
			.WithOne(e => e.User)
			.HasForeignKey(e => e.UserId);

		builder
			.HasMany(e => e.MoneyAccounts)
			.WithOne(e => e.User)
			.HasForeignKey(e => e.UserId);
	}
}