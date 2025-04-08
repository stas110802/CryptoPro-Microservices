using CryptoPro.ClientsService.Domain.Entities;
using CryptoPro.ClientsService.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CryptoPro.ClientsService.Persistence.Data;

public sealed class ClientsDbContext(DbContextOptions<ClientsDbContext> options)
    : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ApiSettingsEntity> ApiSettings { get; set; }
    public DbSet<ExchangeEntity> Exchanges { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfigurations());
        modelBuilder.ApplyConfiguration(new ExchangeConfigurations());
        modelBuilder.ApplyConfiguration(new ApiSettingsConfigurations());

        base.OnModelCreating(modelBuilder);
    }
}