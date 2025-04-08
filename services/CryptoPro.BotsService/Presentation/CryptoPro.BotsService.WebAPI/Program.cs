
using System.Diagnostics;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddMediatR(config =>
//     config.RegisterServicesFromAssembly(typeof(GetAllUsersQuery).Assembly));

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
