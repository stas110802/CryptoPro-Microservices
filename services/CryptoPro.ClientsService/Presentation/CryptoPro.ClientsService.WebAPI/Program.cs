using System.Diagnostics;
using CryptoPro.ClientsService.Application.Profilies;
using CryptoPro.ClientsService.Application.Users.Queries.GetAllUsers;
using CryptoPro.ClientsService.Domain;
using CryptoPro.ClientsService.Domain.Clients.Interfaces;
using CryptoPro.ClientsService.Domain.Repositories;
using CryptoPro.ClientsService.Infrastructure.Clients.Rest.Binance;
using CryptoPro.ClientsService.Infrastructure.Options;
using CryptoPro.ClientsService.Persistence.Data;
using CryptoPro.ClientsService.Persistence.Repositories;
using CryptoPro.ClientsService.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(GetAllUsersQuery).Assembly));


builder.Services.AddDbContext<ClientsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ClientsDbContext)));
});

builder.Services.AddScoped<IApiSettingsRepository, ApiSettingsRepository>();
builder.Services.AddScoped<IExchangeRepository, ExchangeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.Configure<BinanceApiOptions>(options =>
{
    //options.BaseUri = "https://api.binance.com";
    options.BaseUri = "https://testnet.binance.vision";
    options.PublicKey = builder.Configuration["PublicKeys:BinanceTestNet"] ?? string.Empty;
    options.SecretKey = builder.Configuration["SecretKeys:BinanceTestNet"] ?? string.Empty;
});
builder.Services.AddScoped<IRestMarketClient, BinanceRestClient>();
builder.Services.AddScoped<IRestAccountClient, BinanceRestClient>();
builder.Services.AddScoped<IRestTradeClient, BinanceRestClient>();

var app = builder.Build();

var isProduction = app.Environment.IsProduction();
if (isProduction is false)
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTheme(ScalarTheme.Mars)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.RestSharp);
    });
    Process.Start(new ProcessStartInfo(
            "cmd", "/c start http://localhost:5257/scalar/v1")
        { CreateNoWindow = true }
    );
}

await PreparationDb.PrepPopulation(app, isProduction);
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();