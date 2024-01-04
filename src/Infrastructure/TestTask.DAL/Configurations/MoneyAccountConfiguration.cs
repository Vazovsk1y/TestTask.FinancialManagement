using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;
using TestTask.Domain.Constants;

namespace TestTask.DAL.Configurations;

internal class MoneyAccountConfiguration : IEntityTypeConfiguration<MoneyAccount>
{
	public void Configure(EntityTypeBuilder<MoneyAccount> builder)
	{
		builder.ConfigureId<MoneyAccount, MoneyAccountId>();

		builder.Property(e => e.Balance).HasPrecision(Constraints.MoneyAccount.BalancePrecision.Precision, Constraints.MoneyAccount.BalancePrecision.Scale);

		builder.HasOne(e => e.Currency)
			.WithMany()
			.HasForeignKey(e => e.CurrencyId);
	}
}