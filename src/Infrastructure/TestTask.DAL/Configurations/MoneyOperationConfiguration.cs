using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;
using TestTask.Domain.Constants;

namespace TestTask.DAL.Configurations;

internal class MoneyOperationConfiguration : IEntityTypeConfiguration<MoneyOperation>
{
	public void Configure(EntityTypeBuilder<MoneyOperation> builder)
	{
		builder.ConfigureId<MoneyOperation, MoneyOperationId>();

		builder.ConfigureEnumPropertyAsString(e => e.MoveType);

		builder.ConfigureEnumPropertyAsString(e => e.OperationType);

		builder.Property(e => e.MoneyAmount).HasPrecision(Constraints.MoneyOperation.MoneyAmountPrecision.Precision, Constraints.MoneyOperation.MoneyAmountPrecision.Scale);

		builder
			.HasOne(e => e.Commission)
			.WithMany()
			.HasForeignKey(e => e.CommissionId)
			.IsRequired(false);

		builder.ConfigureNullableValueIdProperty(e => e.MoneyAccountFromId);
		builder.ConfigureNullableValueIdProperty(e => e.MoneyAccountToId);
	}
}