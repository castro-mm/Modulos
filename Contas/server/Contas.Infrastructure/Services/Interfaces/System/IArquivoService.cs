using Contas.Core.Dtos.System;
using Contas.Core.Entities.System;
using Microsoft.AspNetCore.Http;

namespace Contas.Infrastructure.Services.Interfaces.System;

public interface IArquivoService : IService<ArquivoDto, Arquivo>
{
    Task<ArquivoDto> SaveFileAsync(IFormFile file, DateTime dataDaUltimaModificacao, CancellationToken cancellationToken);
}
