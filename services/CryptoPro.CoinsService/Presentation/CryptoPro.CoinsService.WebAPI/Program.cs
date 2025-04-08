using CryptoPro.CoinsService.Application.Coins.Queries.GetDetailCoinsInformation;
using CryptoPro.CoinsService.Domain.Interfaces;
using CryptoPro.CoinsService.Infrastructure.Options;
using CryptoPro.CoinsService.Infrastructure.Rest.CoinGecko;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(GetDetailCoinsInformationQuery).Assembly));

// Add exchange Clients
builder.Services.AddScoped<IRestDetailCoinClient, CoinGeckoClient>();

// Configure exchange Options
builder.Services.Configure<CoinGeckoApiOptions>(x =>
{
    x.BaseUri = "https://api.coingecko.com";
    x.PublicKey = builder.Configuration["PublicKeys:CoinGecko"] ?? string.Empty;
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTheme(ScalarTheme.Mars)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.RestSharp);
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();