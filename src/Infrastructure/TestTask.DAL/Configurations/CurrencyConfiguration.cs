using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;

namespace TestTask.DAL.Configurations;

internal class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
	public void Configure(EntityTypeBuilder<Currency> builder)
	{
		builder.ConfigureId<Currency, CurrencyId>();

		builder.HasIndex(e => e.Title).IsUnique();

		builder.HasIndex(e => e.AlphabeticCode).IsUnique();

		builder.HasIndex(e => e.NumericCode).IsUnique();
	}
}