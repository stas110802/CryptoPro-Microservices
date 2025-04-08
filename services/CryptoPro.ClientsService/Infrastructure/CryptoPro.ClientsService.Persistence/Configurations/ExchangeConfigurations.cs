using CryptoPro.ClientsService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoPro.ClientsService.Persistence.Configurations;

public class ExchangeConfigurations : IEntityTypeConfiguration<ExchangeEntity>
{
    public void Configure(EntityTypeBuilder<ExchangeEntity> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .HasMany(e => e.ApiSettings)
            .WithOne(s => s.Exchange)
            .HasForeignKey(s => s.ExchangeId);

        builder
            .Property(e => e.Type)
            .HasConversion<string>();
    }
}