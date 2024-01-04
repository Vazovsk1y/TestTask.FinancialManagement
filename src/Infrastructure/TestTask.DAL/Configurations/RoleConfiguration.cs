using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;

namespace TestTask.DAL.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.ConfigureId<Role,  RoleId>();

		builder.HasIndex(e => e.Title).IsUnique();
	}
}