using Contas.Core.Businesses.Validators;
using Contas.Core.Businesses.Validators.Interfaces;

namespace Contas.Api.Extensions;

public static class BusinessServicesExtensions
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        // Adicione aqui os serviços de negócios, por exemplo:
         services.AddScoped<ISegmentoDoCredorValidator, SegmentoDoCredorValidator>();
         services.AddScoped<ICredorValidator, CredorValidator>();
         services.AddScoped<IPagadorValidator, PagadorValidator>();
         services.AddScoped<IRegistroDaContaValidator, RegistroDaContaValidator>();
         services.AddScoped<IArquivoDoRegistroDaContaValidator, ArquivoDoRegistroDaContaValidator>();
         services.AddScoped<ITrilhaDeAuditoriaValidator, TrilhaDeAuditoriaValidator>();
         services.AddScoped<IArquivoValidator, ArquivoValidator>();
    }
}
