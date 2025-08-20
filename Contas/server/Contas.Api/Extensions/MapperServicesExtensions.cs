using Contas.Core.Interfaces.Repositories;
using Contas.Infrastructure.Data.Repositories;
using Contas.Infrastructure.Services;
using Contas.Infrastructure.Services.Interfaces;
using Contas.Infrastructure.Services.Interfaces.System;
using Contas.Infrastructure.Services.System;

namespace Contas.Api.Extensions;

public static class MapperServicesExtensions
{
    public static void AddMappingServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISegmentoDoCredorService, SegmentoDoCredorService>();
        services.AddScoped<ILogDeErroService, LogDeErroService>();
        services.AddScoped<ITrilhaDeAuditoriaService, TrilhaDeAuditoriaService>();
        services.AddScoped<IArquivoService, ArquivoService>();
        services.AddScoped<IRegistroDaContaService, RegistroDaContaService>();
        services.AddScoped<ICredorService, CredorService>();
        services.AddScoped<IPagadorService, PagadorService>();
    }
}