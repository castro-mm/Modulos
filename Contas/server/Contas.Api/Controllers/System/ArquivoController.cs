using Contas.Api.Controllers.Base;
using Contas.Api.Objects;
using Contas.Core.Dtos.System;
using Contas.Core.Entities.System;
using Contas.Infrastructure.Services.Interfaces.System;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers.System;

public class ArquivoController : BaseApiController<ArquivoDto, Arquivo>
{
    private readonly IArquivoService service;

    public ArquivoController(IArquivoService service) : base(service)
    {
        this.service = service;
    }

    [HttpPost("upload")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UploadFileAsync([FromForm] IFormFile file, [FromForm] DateTime dataDaUltimaModificacao, CancellationToken cancellationToken)
    {        
        var result = await service.SaveFileAsync(file, dataDaUltimaModificacao, cancellationToken);
        return Ok(result);
    }     

    [HttpGet("download/{id:int}")]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]    
    public async Task<ActionResult> DownloadFileAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        var arquivoDto = await service.GetByIdAsync(id, cancellationToken);

        if (arquivoDto == null)
            return NotFound("Arquivo não encontrado.");

        return File(arquivoDto.Dados, arquivoDto.Tipo, $"{arquivoDto.Nome}.{arquivoDto.Extensao}");
    }    
}
