using CryptoPro.ClientsService.Domain.Entities;
using CryptoPro.ClientsService.Domain.Types;
using CryptoPro.ClientsService.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CryptoPro.ClientsService.WebAPI.Data;

public class PreparationDb
{
    public static async Task PrepPopulation(IApplicationBuilder app, bool isProduction)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        await SetData(serviceScope.ServiceProvider.GetRequiredService<ClientsDbContext>(), isProduction);
    }

    private static async Task SetData(ClientsDbContext context, bool isProduction)
    {
        if (isProduction)
        {
            Console.WriteLine("Attempting to apply migrations...");
            try
            {
               await context.Database.MigrateAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot migrate database, see inner exception.");
                Console.WriteLine(e.Message);
            }
            
        }
        
        if (context.Exchanges.Any() is false)
        {
            Console.WriteLine("Adding supported exchanges...");
            context.Exchanges.AddRange(
                new ExchangeEntity { Type = ExchangeType.Binance },
                new ExchangeEntity { Type = ExchangeType.CoinGecko }
            );
            await context.SaveChangesAsync();
        }
        else
        {
            Console.WriteLine("Already have data in db!");
        }
    }
}