using Contas.Api.Extensions;
using Contas.Api.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInterceptorsServices();
builder.Services.AddDatabaseServices(builder);
builder.Services.AddMappingServices();
builder.Services.AddControllersServices();

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<RequestApiMiddleware>();
app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();