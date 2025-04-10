using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using CryptoPro.BotsService.Application.Orders.Queries.GetUserSltpOrders;
using CryptoPro.BotsService.Application.Repositories;
using CryptoPro.BotsService.Application.Services;
using CryptoPro.BotsService.Application.Services.Backgrounds;
using CryptoPro.BotsService.Domain;
using CryptoPro.BotsService.Domain.Repositories;
using CryptoPro.BotsService.Infrastructure.Clients;
using CryptoPro.BotsService.Infrastructure.Redis;
using CryptoPro.BotsService.Persistence.Data;
using CryptoPro.BotsService.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();

// PostreSQL
builder.Services.AddDbContext<BotsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(BotsDbContext)));
});

// Redis
var redisConnection = builder.Configuration.GetConnectionString("Redis");

// builder.Services.AddStackExchangeRedisCache(opt =>
// {
//     opt.Configuration = "redis.default.svc.cluster.local:6379,abortConnect=false";
// });

builder.Services.AddSingleton<IConnectionMultiplexer>(sp => 
    ConnectionMultiplexer.Connect(redisConnection));
builder.Services.AddScoped<IRedisService, RedisCacheService>();

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
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)),
            RoleClaimType = ClaimTypes.Role
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IJwtParser, JwtParser>();

//Repositories
builder.Services.AddScoped<ISltpOrderRepository, SltpOrderRepository>();

// Infrastructure Layer
builder.Services.AddScoped<ICryptoProClientService, CryptoProClientService>();

// Application Layer
builder.Services.AddSingleton<IBotStateRepository, BotStateRepository>();
builder.Services.AddScoped<IBotService, BotService>();
builder.Services.AddHostedService<BotBackgroundService>();

// MediatR
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