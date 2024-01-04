using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;

namespace TestTask.DAL.Configurations;

internal class MoneyAccountConfiguration : IEntityTypeConfiguration<MoneyAccount>
{
	public void Configure(EntityTypeBuilder<MoneyAccount> builder)
	{
		builder.ConfigureId<MoneyAccount, MoneyAccountId>();

		builder.HasOne<Currency>()
			.WithMany()
			.HasForeignKey(e => e.CurrencyId);
	}
}