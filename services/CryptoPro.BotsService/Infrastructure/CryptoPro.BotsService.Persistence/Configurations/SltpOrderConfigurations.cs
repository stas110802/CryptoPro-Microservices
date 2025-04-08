using CryptoPro.BotsService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoPro.BotsService.Persistence.Configurations;

public class SltpOrderConfigurations : IEntityTypeConfiguration<SltpOrderEntity>
{
    public void Configure(EntityTypeBuilder<SltpOrderEntity> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Currency)
            .IsRequired();
        builder
            .Property(e => e.Amount)
            .IsRequired();
        builder
            .Property(e => e.SellPrice)
            .IsRequired();
        builder
            .Property(e => e.UpperPrice)
            .IsRequired();
        builder
            .Property(e => e.BottomPrice)
            .IsRequired();
        builder
            .Property(e => e.Type)
            .IsRequired();
        builder
            .Property(e => e.ExchangeId)
            .IsRequired();
        builder
            .Property(e => e.UserId)
            .IsRequired();

        builder
            .Property(e => e.Type)
            .HasConversion<string>();
    }
}