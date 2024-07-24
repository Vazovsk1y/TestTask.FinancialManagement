using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.DAL.SQLServer.Extensions;
using TestTask.Domain.Entities;
using TestTask.Domain.Constants;

namespace TestTask.DAL.SQLServer.Configurations;

internal class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
	public void Configure(EntityTypeBuilder<Currency> builder)
	{
		builder.ConfigureId<Currency, CurrencyId>();

		builder.Property(e => e.Title).HasMaxLength(Constraints.Currency.MaxTitleLength);

		builder.HasIndex(e => e.Title).IsUnique();

		builder.Property(e => e.AlphabeticCode).HasMaxLength(Constraints.CurrencyCodes.CodeMaxLength);
		builder.HasIndex(e => e.AlphabeticCode).IsUnique();

		builder.Property(e => e.NumericCode).HasMaxLength(Constraints.CurrencyCodes.CodeMaxLength);
		builder.HasIndex(e => e.NumericCode).IsUnique();
	}
}