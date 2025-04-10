using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CryptoPro.ClientsService.Application.Profilies;
using CryptoPro.ClientsService.Application.Users.Queries.GetAllUsers;
using CryptoPro.ClientsService.Domain.Clients.Interfaces;
using CryptoPro.ClientsService.Domain.Repositories;
using CryptoPro.ClientsService.Infrastructure.Clients.Rest.Binance;
using CryptoPro.ClientsService.Infrastructure.Options;
using CryptoPro.ClientsService.Persistence.Data;
using CryptoPro.ClientsService.Persistence.Repositories;
using CryptoPro.ClientsService.WebAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//builder.Services.AddOpenApi();
builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

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
    options.BaseUri = "https://testnet.binance.vision";
    options.PublicKey = builder.Configuration["PublicKeys:BinanceTestNet"] ?? string.Empty;
    options.SecretKey = builder.Configuration["SecretKeys:BinanceTestNet"] ?? string.Empty;
});
builder.Services.AddScoped<IRestMarketClient, BinanceRestClient>();
builder.Services.AddScoped<IRestAccountClient, BinanceRestClient>();
builder.Services.AddScoped<IRestTradeClient, BinanceRestClient>();

// JWT
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

internal sealed class BearerSecuritySchemeTransformer(
    Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider authenticationSchemeProvider)
    : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
        if (authenticationSchemes.Any(authScheme => authScheme.Name == "Bearer"))
        {
            var requirements = new Dictionary<string, OpenApiSecurityScheme>
            {
                ["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    In = ParameterLocation.Header,
                    BearerFormat = "Json Web Token"
                }
            };
            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes = requirements;

            foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations))
            {
                operation.Value.Security.Add(new OpenApiSecurityRequirement
                {
                    [new OpenApiSecurityScheme { Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme } }] =
                        Array.Empty<string>()
                });
            }
        }
    }
}