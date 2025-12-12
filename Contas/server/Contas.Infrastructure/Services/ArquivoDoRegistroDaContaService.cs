using Contas.Core.Dtos;
using Contas.Core.Dtos.System;
using Contas.Core.Entities;
using Contas.Core.Interfaces.Repositories;
using Contas.Core.Mappings;
using Contas.Infrastructure.Services.Base;
using Contas.Infrastructure.Services.Interfaces;
using Contas.Infrastructure.Services.Interfaces.System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using static Contas.Core.Objects.Enumerations;

namespace Contas.Infrastructure.Services;

public class ArquivoDoRegistroDaContaService : Service<ArquivoDoRegistroDaContaDto, ArquivoDoRegistroDaConta>, IArquivoDoRegistroDaContaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;    
    private readonly IArquivoDoRegistroDaContaRepository _repository;
    private readonly IArquivoService _arquivoService;

    public ArquivoDoRegistroDaContaService(
        IUnitOfWork unitOfWork, 
        IConfiguration configuration, 
        IArquivoDoRegistroDaContaRepository repository,
        IArquivoService arquivoService
        ) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _repository = repository;
        _arquivoService = arquivoService;
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
            // Arquivo = new()
            // {
            //     Nome = Path.GetFileNameWithoutExtension(file.FileName).ToLowerInvariant(),
            //     Extensao = Path.GetExtension(file.FileName).ToLowerInvariant(),
            //     Tamanho = file.Length,
            //     Dados = arquivo.Dados,
            //     DataDeAtualizacao = DateTime.Now,
            //     DataDeCriacao = DateTime.Now,
            // },
            DataDeAtualizacao = DateTime.Now,
            DataDeCriacao = DateTime.Now,
        };

        return await base.CreateAsync(arquivoDoRegistroDaContaDto, cancellationToken);
    }

    public async Task<List<ArquivoDoRegistroDaContaDto>> GetFilesByRegistroDaContaIdAsync(int registroDaContaId, CancellationToken cancellationToken)
    {
        var arquivos = await _repository.FindByRegistroDaContaIdAsync(registroDaContaId, cancellationToken);
        
        return arquivos.Select(x => x.ToDto()).ToList(); 
    }
}
