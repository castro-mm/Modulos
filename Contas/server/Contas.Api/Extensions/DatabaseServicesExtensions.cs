using Contas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Contas.Api.Extensions;

public static class DatabaseServicesExtensions
{
    public static void AddDatabaseServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ContasContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }
}
