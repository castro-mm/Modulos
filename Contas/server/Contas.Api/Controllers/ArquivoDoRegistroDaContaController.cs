using Contas.Api.Controllers.Base;
using Contas.Api.Objects;
using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Objects;
using Contas.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Contas.Core.Objects.Enumerations;

namespace Contas.Api.Controllers;

public class ArquivoDoRegistroDaContaController : BaseApiController<ArquivoDoRegistroDaContaDto, ArquivoDoRegistroDaConta>
{
    private readonly IArquivoDoRegistroDaContaService _service;
    private readonly IArquivoDoRegistroDaContaValidator _validator;
    
    public ArquivoDoRegistroDaContaController(IArquivoDoRegistroDaContaService service, IArquivoDoRegistroDaContaValidator validator) : base(service, validator)
    {
        _service = service;
        _validator = validator;
    }

    [HttpGet("get-by-registro-da-conta-id/{registroDaContaId:int}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]    
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]    
    public async Task<ActionResult> GetFilesByRegistroDaContaIdAsync([FromRoute] int registroDaContaId, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(registroDaContaId);

        if (!validationResult.IsValid)
            return BadRequest(Result.Failure(validationResult.Errors));
        
        var result = await _service.GetFilesByRegistroDaContaIdAsync(registroDaContaId, cancellationToken);

        return Ok(Result.Successful<IEnumerable<ArquivoDoRegistroDaContaDto>>(result));
    }

    [HttpPost("save")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SaveFileAsync([FromForm] int registroDaContaId, [FromForm] ModalidadeDoArquivo tipoDeArquivo, [FromForm] DateTime dataDaUltimaModificacao, [FromForm] IFormFile file, CancellationToken cancellationToken)
    {
        // implementar as validações do upload aqui     
           
        var result = await _service.SaveFileAsync(registroDaContaId, tipoDeArquivo, dataDaUltimaModificacao, file, cancellationToken);

        return Ok(Result<ArquivoDoRegistroDaContaDto>.Successful(result));
    }  

    [HttpGet("download/{arquivoId:int}")]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DownloadFileAsync([FromRoute] int arquivoId, CancellationToken cancellationToken)
    {
        var arquivo = (await _service.GetByIdAsync(arquivoId, cancellationToken)).Arquivo;

        if (arquivo == null) return NotFound("Arquivo não encontrado.");

        var nomeCompleto = $"{arquivo.Nome}{arquivo.Extensao}";
        var contentType = GetContentType(arquivo.Extensao);

        // Adicionar headers para evitar problemas com HTTP/2
        // Response.Headers.Append("Content-Disposition", $"attachment; filename=\"{nomeCompleto}\"");
        // Response.Headers.Append("Content-Length", arquivo.Dados.Length.ToString());
        // Response.Headers.Append("Content-Type", contentType);
        
        // Usar MemoryStream para evitar problemas com buffering
        var stream = new MemoryStream(arquivo.Dados);
        return new FileStreamResult(stream, contentType)
        {
            FileDownloadName = nomeCompleto,
            //EnableRangeProcessing = false
        };
    }
    
    private static string GetContentType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return extension switch
        {
            ".pdf" => "application/pdf",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".txt" => "text/plain",
            ".zip" => "application/zip",
            _ => "application/octet-stream"
        };
    }
    
}
