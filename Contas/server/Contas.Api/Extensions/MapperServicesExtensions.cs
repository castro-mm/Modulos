using Contas.Core.Data.Repositories;
using Contas.Core.Interfaces.Repositories;
using Contas.Core.Interfaces.Services;
using Contas.Core.Interfaces.Services.Security;
using Contas.Core.Interfaces.Services.System;
using Contas.Core.Services.Security;
using Contas.Infrastructure.Services;
using Contas.Infrastructure.Services.Dashboard;
using Contas.Infrastructure.Services.Security;
using Contas.Infrastructure.Services.System;

namespace Contas.Api.Extensions;

public static class MapperServicesExtensions
{
    public static void AddMappingServices(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();        
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IArquivoDoRegistroDaContaRepository, ArquivoDoRegistroDaContaRepository>();
        services.AddScoped<IRegistroDaContaRepository, RegistroDaContaRepository>();

        services.AddScoped<ISegmentoDoCredorService, SegmentoDoCredorService>();
        services.AddScoped<ILogDeErroService, LogDeErroService>();
        services.AddScoped<ITrilhaDeAuditoriaService, TrilhaDeAuditoriaService>();
        services.AddScoped<IArquivoService, ArquivoService>();
        services.AddScoped<IArquivoDoRegistroDaContaService, ArquivoDoRegistroDaContaService>();
        services.AddScoped<IRegistroDaContaService, RegistroDaContaService>();
        services.AddScoped<ICredorService, CredorService>();
        services.AddScoped<IPagadorService, PagadorService>();
        services.AddScoped<IDashboardService, DashboardService>();
    }
}