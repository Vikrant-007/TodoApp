using Serilog;
using TodoApp.Application.DependencyInjection;
using TodoApp.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//Cleared the default logging providers and added Serilog
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseInfrastructurePolicies();

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
