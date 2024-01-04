using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;

namespace TestTask.DAL.Configurations;

internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
	public void Configure(EntityTypeBuilder<UserRole> builder)
	{
		builder.HasKey(e => new { e.UserId, e.RoleId });
	}
}