using Contas.Core.Interfaces.Repositories;
using Contas.Core.Interfaces.Services;
using Contas.Infrastructure.Data.Repositories;
using Contas.Infrastructure.Services;

namespace Contas.Api.Extensions;

public static class MapperServicesExtensions
{
    public static void AddMappingServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISegmentoDoCredorService, SegmentoDoCredorService>();
    }
}
