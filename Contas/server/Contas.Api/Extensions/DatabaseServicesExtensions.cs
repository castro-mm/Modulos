using Contas.Infrastructure.Data;
using Contas.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Contas.Api.Extensions;

public static class DatabaseServicesExtensions
{
    public static void AddDatabaseServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<AuditSaveChangesInterceptor>();

        services.AddDbContext<ContasContext>(options =>
        {
            options
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                .UseLazyLoadingProxies();
            
            if (builder.Environment.IsDevelopment())
                options.EnableSensitiveDataLogging();
        });
    }
}
