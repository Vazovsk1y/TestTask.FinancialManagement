using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.DAL.SQLServer.Extensions;
using TestTask.Domain.Entities;
using TestTask.Domain.Constants;

namespace TestTask.DAL.SQLServer.Configurations;

internal class CommissionConfiguration : IEntityTypeConfiguration<Commission>
{
	public void Configure(EntityTypeBuilder<Commission> builder)
	{
		builder.ConfigureId<Commission, CommissionId>();

		builder.Property(e => e.Value).HasPrecision(Constraints.Commission.ValuePrecision.Precision, Constraints.Commission.ValuePrecision.Scale);

		builder.HasOne(e => e.From)
			.WithMany()
			.HasForeignKey(e => e.CurrencyFromId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(e => e.To)
			.WithMany()
			.HasForeignKey(e => e.CurrencyToId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}