using CryptoPro.ExchangeClients.Domain.Clients;
using CryptoPro.ExchangeClients.Infrastructure.Clients.Rest.CoinGecko;
using CryptoPro.ExchangeClients.Infrastructure.RestAPI.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Add exchange Clients
builder.Services.AddScoped<IRestDetailCoinClient, CoinGeckoClient>();

// Configure exchange Options
builder.Services.Configure<ExchangeApiOptions>(x =>
{
   
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();