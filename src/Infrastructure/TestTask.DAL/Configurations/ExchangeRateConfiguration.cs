using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;
using TestTask.Domain.Constants;

namespace TestTask.DAL.Configurations;

internal class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.ConfigureId<ExchangeRate, ExchangeRateId>();

        builder.Property(e => e.Value).HasPrecision(Constraints.ExchangeRate.ValuePrecision.Precision, Constraints.ExchangeRate.ValuePrecision.Scale);

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