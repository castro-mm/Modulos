using API.Extensions;
using API.Middlewares;
using Core.Entities.Identity;
using Infrastructure.Data;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:4200"));

// These two Middlewares must be declared AFTER the UseCors, and BEFORE the MapControllers
app.UseAuthentication(); // Asks if you have a valid token
app.UseAuthorization(); // Check what you are allowed to do on the app

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
app.MapControllers();

await app.SeedDataAsync();

app.Run();