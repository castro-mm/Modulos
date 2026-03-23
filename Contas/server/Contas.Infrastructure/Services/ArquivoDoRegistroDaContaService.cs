using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Interfaces.Repositories;
using Contas.Core.Interfaces.Services;
using Contas.Core.Interfaces.Services.Security;
using Contas.Core.Interfaces.Services.System;
using Contas.Core.Mappings;
using Contas.Infrastructure.Services.Base;
using Microsoft.AspNetCore.Http;
using static Contas.Core.Objects.Enumerations;

namespace Contas.Infrastructure.Services;

public class ArquivoDoRegistroDaContaService : Service<ArquivoDoRegistroDaContaDto, ArquivoDoRegistroDaConta>, IArquivoDoRegistroDaContaService
{
    private readonly IArquivoDoRegistroDaContaRepository _repository;
    private readonly IArquivoService _arquivoService;
    private readonly ICurrentUserService _currentUserService;

    public ArquivoDoRegistroDaContaService(
        IUnitOfWork unitOfWork, 
        IArquivoDoRegistroDaContaRepository repository,
        IArquivoService arquivoService,
        ICurrentUserService currentUserService
        ) : base(unitOfWork, currentUserService)
    {
        _repository = repository;
        _arquivoService = arquivoService;
        _currentUserService = currentUserService;
    }

    public async Task<ArquivoDoRegistroDaContaDto> SaveFileAsync(int registroDaContaId, ModalidadeDoArquivo tipoDeArquivo, DateTime dataDaUltimaModificacao, IFormFile file, CancellationToken cancellationToken)
    {       
        var arquivo = await _arquivoService.SaveFileAsync(file, dataDaUltimaModificacao, cancellationToken);
        
        ArquivoDoRegistroDaContaDto arquivoDoRegistroDaContaDto = new()
        {
            RegistroDaContaId = registroDaContaId,
            ArquivoId = arquivo.Id,
            ModalidadeDoArquivo = tipoDeArquivo,
            Arquivo = arquivo,
            DataDeAtualizacao = DateTime.Now,
            DataDeCriacao = DateTime.Now,
            UserId = _currentUserService.UserId
        };

        return await base.CreateAsync(arquivoDoRegistroDaContaDto, cancellationToken);
    }

    public async Task<List<ArquivoDoRegistroDaContaDto>> GetFilesByRegistroDaContaIdAsync(int registroDaContaId, CancellationToken cancellationToken)
    {
        var arquivos = await _repository.FindByRegistroDaContaIdAsync(registroDaContaId, cancellationToken);
        
        return [..arquivos.Select(x => x.ToDto())];
    }
}
