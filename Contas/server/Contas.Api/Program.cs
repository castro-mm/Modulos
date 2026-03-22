using Contas.Api.Extensions;
using Contas.Api.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInterceptorsServices();
builder.Services.AddDatabaseServices(builder);
builder.Services.AddMappingServices();
builder.Services.AddControllersServices();
builder.Services.AddBusinessServices();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddEmailServices(builder.Configuration);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});

var app = builder.Build();

// Seed de Roles e Admin padrão
await app.SeedIdentityAsync();

// Configure the HTTP request pipeline.
app.UseMiddleware<RequestApiMiddleware>();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

app.UseCors(x => x.AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials()
                  .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();