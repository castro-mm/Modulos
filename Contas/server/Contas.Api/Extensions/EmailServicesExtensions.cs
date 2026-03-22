using Contas.Core.Interfaces.Services;
using Contas.Infrastructure.Objects;
using Contas.Infrastructure.Services;

namespace Contas.Api.Extensions;

public static class EmailServicesExtensions
{
    public static void AddEmailServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("Email"));
        services.AddScoped<IEmailService, EmailService>();
    }
}
