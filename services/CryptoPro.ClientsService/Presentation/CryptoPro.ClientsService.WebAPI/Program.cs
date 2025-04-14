using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using CryptoPro.ClientsService.Application.Interfaces;
using CryptoPro.ClientsService.Application.Profilies;
using CryptoPro.ClientsService.Application.Users.Queries.GetAllUsers;
using CryptoPro.ClientsService.Domain.Repositories;
using CryptoPro.ClientsService.Infrastructure.Factories;
using CryptoPro.ClientsService.Persistence.Data;
using CryptoPro.ClientsService.Persistence.Repositories;
using CryptoPro.ClientsService.WebAPI.Data;
using CryptoPro.Common.Utilities.Schemes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(GetAllUsersQuery).Assembly));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<ClientsDbContext>(options =>
{
    options.UseNpgsql("Host=postgres-clients-internal;Username=AkiraKilla;Password=SuperSecretPassword;Database=clientsServiceDb");
});

builder.Services.AddScoped<IApiSettingsRepository, ApiSettingsRepository>();
builder.Services.AddScoped<IExchangeRepository, ExchangeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IExchangeClientFactory, ExchangeClientFactory>();

// JWT
Console.WriteLine("Adding jwt...");
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
            RoleClaimType = ClaimTypes.Role
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

var isProduction = app.Environment.IsProduction();
//if (isProduction is false)
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTheme(ScalarTheme.Mars)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.RestSharp);
    });
    // Process.Start(new ProcessStartInfo(
    //         "cmd", "/c start http://localhost:5257/scalar/v1")
    //     { CreateNoWindow = true }
    // );
}

await PreparationDb.PrepPopulation(app, isProduction);
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();