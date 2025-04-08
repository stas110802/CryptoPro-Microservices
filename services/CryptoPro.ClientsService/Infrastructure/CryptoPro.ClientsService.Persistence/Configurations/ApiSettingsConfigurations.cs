using CryptoPro.ClientsService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoPro.ClientsService.Persistence.Configurations;

public class ApiSettingsConfigurations : IEntityTypeConfiguration<ApiSettingsEntity>
{
    public void Configure(EntityTypeBuilder<ApiSettingsEntity> builder)
    {
        builder
            .HasKey(e => e.Id);
        
        builder
            .Property(s => s.SpecificSettings)
            .HasColumnType("json");

        builder
            .HasOne(s => s.User)
            .WithMany(u => u.ApiSettings)
            .HasForeignKey(s => s.UserId);
        
        builder
            .HasOne(s => s.Exchange)
            .WithMany(u => u.ApiSettings)
            .HasForeignKey(s => s.ExchangeId);
    }
}