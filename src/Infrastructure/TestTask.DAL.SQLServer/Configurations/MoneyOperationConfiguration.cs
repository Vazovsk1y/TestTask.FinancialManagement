using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.DAL.SQLServer.Extensions;
using TestTask.Domain.Entities;
using TestTask.Domain.Constants;

namespace TestTask.DAL.SQLServer.Configurations;

internal class MoneyOperationConfiguration : IEntityTypeConfiguration<MoneyOperation>
{
	public void Configure(EntityTypeBuilder<MoneyOperation> builder)
	{
		builder.ConfigureId<MoneyOperation, MoneyOperationId>();

		builder.ConfigureEnumPropertyAsString(e => e.MoveType);

		builder.ConfigureEnumPropertyAsString(e => e.OperationType);

		builder.Property(e => e.MoneyAmount).HasPrecision(Constraints.MoneyOperation.MoneyAmountPrecision.Precision, Constraints.MoneyOperation.MoneyAmountPrecision.Scale);

		builder.Property(e => e.AppliedCommissionValue).HasPrecision(Constraints.Commission.ValuePrecision.Precision, Constraints.Commission.ValuePrecision.Precision);
		builder.Property(e => e.AppliedExchangeRate).HasPrecision(Constraints.MoneyOperation.MoneyAmountPrecision.Precision, Constraints.MoneyOperation.MoneyAmountPrecision.Scale);

        builder.HasOne(e => e.From)
            .WithMany()
            .HasForeignKey(e => e.MoneyAccountFromId);

        builder.HasOne(e => e.To)
            .WithMany()
            .HasForeignKey(e => e.MoneyAccountToId);
    }
}