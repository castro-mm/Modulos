using Contas.Api.Extensions;
using Contas.Api.Middleware;
using Serilog;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

////Configurar Kestrel para usar HTTP/1.1
// builder.WebHost.ConfigureKestrel(serverOptions =>
// {
//     serverOptions.ConfigureEndpointDefaults(listenOptions =>
//     {
//         listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
//     });
// });

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
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

app.UseCors(x => x.AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials()
                  .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.MapControllers();

app.Run();