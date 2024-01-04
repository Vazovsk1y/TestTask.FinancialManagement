using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;

namespace TestTask.DAL.Configurations;

internal class MoneyOperationConfiguration : IEntityTypeConfiguration<MoneyOperation>
{
	public void Configure(EntityTypeBuilder<MoneyOperation> builder)
	{
		builder.ConfigureId<MoneyOperation, MoneyOperationId>();

		builder.ConfigureEnumPropertyAsString(e => e.MoveType);

		builder.ConfigureEnumPropertyAsString(e => e.OperationType);
	}
}