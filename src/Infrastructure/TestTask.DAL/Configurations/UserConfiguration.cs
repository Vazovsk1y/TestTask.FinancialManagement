using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;
using TestTask.Domain.Constants;

namespace TestTask.DAL.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ConfigureId<User, UserId>();

		builder.HasIndex(u => u.Email).IsUnique();

		builder.Property(e => e.Email).HasMaxLength(Constraints.User.MaxEmailLength);
		builder.Property(e => e.FullName).HasMaxLength(Constraints.User.MaxFullNameLength);

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