using CryptoPro.ClientsService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoPro.ClientsService.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .HasMany(u => u.ApiSettings)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId);
        
        builder
            .Property(u => u.Username)
            .IsRequired();
        
        builder
            .Property(u => u.HashPassword)
            .IsRequired();
        
        builder
            .Property(u => u.Login)
            .IsRequired();
    }
}