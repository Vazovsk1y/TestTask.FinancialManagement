using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;

namespace TestTask.DAL.Configurations;

internal class CommissionConfiguration : IEntityTypeConfiguration<Commission>
{
	public void Configure(EntityTypeBuilder<Commission> builder)
	{
		builder.ConfigureId<Commission, CommissionId>();
	}
}