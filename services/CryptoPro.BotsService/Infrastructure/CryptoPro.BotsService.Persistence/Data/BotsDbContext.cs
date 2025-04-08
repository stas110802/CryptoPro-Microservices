using CryptoPro.BotsService.Domain.Entities;
using CryptoPro.BotsService.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CryptoPro.BotsService.Persistence.Data;

public sealed class BotsDbContext(DbContextOptions<BotsDbContext> options)
    : DbContext(options)
{
    public DbSet<SltpOrderEntity> SltpOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SltpOrderConfigurations());

        base.OnModelCreating(modelBuilder);
    }
}