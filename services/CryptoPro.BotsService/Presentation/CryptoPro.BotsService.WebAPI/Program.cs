using System.Diagnostics;
using CryptoPro.BotsService.Application.Orders.Queries.GetUserSltpOrders;
using CryptoPro.BotsService.Application.Repositories;
using CryptoPro.BotsService.Application.Services;
using CryptoPro.BotsService.Application.Services.Backgrounds;
using CryptoPro.BotsService.Domain;
using CryptoPro.BotsService.Domain.Repositories;
using CryptoPro.BotsService.Infrastructure.Clients;
using CryptoPro.BotsService.Persistence.Data;
using CryptoPro.BotsService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<BotsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(BotsDbContext)));
});


builder.Services.AddScoped<ISltpOrderRepository, SltpOrderRepository>();
builder.Services.AddScoped<ICryptoProClientService, CryptoProClientService>();

builder.Services.AddSingleton<IBotStateRepository, BotStateRepository>();
builder.Services.AddScoped<IBotService, BotService>();
builder.Services.AddHostedService<BotBackgroundService>();

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(GetUserSltpOrdersQuery).Assembly));

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
            "cmd", "/c start http://localhost:5149/scalar/v1")
        { CreateNoWindow = true }
    );
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();