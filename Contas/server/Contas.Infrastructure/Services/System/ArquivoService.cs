using Contas.Core.Dtos.System;
using Contas.Core.Entities.System;
using Contas.Core.Interfaces.Repositories;
using Contas.Infrastructure.Services.Base;
using Contas.Infrastructure.Services.Interfaces.System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Contas.Infrastructure.Services.System;

public class ArquivoService : Service<ArquivoDto, Arquivo>, IArquivoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public ArquivoService(IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<ArquivoDto> SaveFileAsync(IFormFile file, DateTime dataDaUltimaModificacao, CancellationToken cancellationToken)
    {
        // This rules are going to be implemented based on Specification Pattern later
        if (file == null) throw new ArgumentException("Arquivo não enviado.", nameof(file));

        // read size from file and compare with config value
        var tamanhoMaximo = long.Parse(_configuration.GetSection("ConfiguracaoDoArquivo:TamanhoMaximoDoArquivoEmBytes").Value!);
        if (file.Length > tamanhoMaximo) 
            throw new ArgumentException($"O tamanho do arquivo não pode exceder {tamanhoMaximo} bytes.", nameof(file));

        var tiposPermitidos = _configuration.GetSection("ConfiguracaoDoArquivo:FormatoPermitido").Value?.Split(',');
        if (tiposPermitidos != null && !tiposPermitidos.Contains(Path.GetExtension(file.FileName).TrimStart('.')))
            throw new ArgumentException("Formato de arquivo não permitido.", nameof(file));

        using var memoryStream = new MemoryStream();

        await file.CopyToAsync(memoryStream, cancellationToken);

        return await base.CreateAsync(
            new ArquivoDto
            {
                Nome = Path.GetFileNameWithoutExtension(file.FileName).ToLowerInvariant(),
                Extensao = Path.GetExtension(file.FileName).ToLowerInvariant(),
                Tamanho = file.Length,
                Tipo = file.ContentType,
                DataDaUltimaModificacao = dataDaUltimaModificacao,
                Dados = memoryStream.ToArray(),
                DataDeAtualizacao = DateTime.Now,
                DataDeCriacao = DateTime.Now,
            },
            cancellationToken
        ) ?? throw new InvalidOperationException("Erro ao salvar o arquivo.");
    }        
}
