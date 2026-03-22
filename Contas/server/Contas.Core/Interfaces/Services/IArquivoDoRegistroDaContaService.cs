using Contas.Core.Dtos;
using Contas.Core.Entities;
using Microsoft.AspNetCore.Http;
using static Contas.Core.Objects.Enumerations;

namespace Contas.Core.Interfaces.Services;

public interface IArquivoDoRegistroDaContaService : IService<ArquivoDoRegistroDaContaDto, ArquivoDoRegistroDaConta>
{
    Task<ArquivoDoRegistroDaContaDto> SaveFileAsync(int registroDaContaId, ModalidadeDoArquivo tipoDeArquivo, DateTime dataDaUltimaModificacao, IFormFile file, CancellationToken cancellationToken);
    Task<List<ArquivoDoRegistroDaContaDto>> GetFilesByRegistroDaContaIdAsync(int registroDaContaId, CancellationToken cancellationToken);
}
