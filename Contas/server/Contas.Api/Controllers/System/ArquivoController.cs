using Contas.Api.Controllers.Base;
using Contas.Api.Objects;
using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos.System;
using Contas.Core.Entities.System;
using Contas.Core.Objects;
using Contas.Infrastructure.Services.Interfaces.System;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers.System;

public class ArquivoController : BaseApiController<ArquivoDto, Arquivo>
{
    private readonly IArquivoService _service;
    private readonly IArquivoValidator _validator;

    public ArquivoController(IArquivoService service, IArquivoValidator validator) : base(service, validator)
    {
        _service = service;
        _validator = validator;
    }

    [HttpPost("upload")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UploadFileAsync([FromForm] IFormFile file, [FromForm] DateTime dataDaUltimaModificacao, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(file);
        if (!validationResult.IsValid)
            return BadRequest(Result.Failure(validationResult.Errors));

        var result = await _service.SaveFileAsync(file, dataDaUltimaModificacao, cancellationToken);

        if (result == null)
            validationResult.AddError("ERRO_SALVAR_ARQUIVO", "Erro ao salvar o arquivo.");

        return validationResult.IsValid 
            ? Ok(Result.Successful(result))
            : BadRequest(Result.Failure(validationResult.Errors)); 
    }     

    [HttpGet("download/{id:int}")]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]    
    public async Task<ActionResult> DownloadFileAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(id);
        if (!validationResult.IsValid)
            return BadRequest(Result.Failure(validationResult.Errors));

        var arquivoDto = await _service.GetByIdAsync(id, cancellationToken);

        if (arquivoDto == null)
            return NotFound(Result.Failure(null!, "Arquivo não encontrado."));

        return File(arquivoDto.Dados, arquivoDto.Tipo, $"{arquivoDto.Nome}.{arquivoDto.Extensao}");
    }    
}
