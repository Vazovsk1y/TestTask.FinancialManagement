using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;
using TestTask.Domain.Constants;

namespace TestTask.DAL.Configurations;

internal class CommissionConfiguration : IEntityTypeConfiguration<Commission>
{
	public void Configure(EntityTypeBuilder<Commission> builder)
	{
		builder.ConfigureId<Commission, CommissionId>();

		builder.Property(e => e.Value).HasPrecision(Constraints.Commission.ValuePrecision.Precision, Constraints.Commission.ValuePrecision.Scale);

		builder.ConfigureValueIdProperty(e => e.CurrencyFromId);
		builder.ConfigureValueIdProperty(e => e.CurrencyToId);
	}
}