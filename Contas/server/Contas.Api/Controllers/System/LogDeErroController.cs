using Contas.Core.Dtos.System;
using Contas.Core.Objects;
using Contas.Infrastructure.Services.Interfaces.System;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers.System;

[Route("api/[controller]")]
[ApiController]
public class LogDeErroController(ILogDeErroService logDeErroService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<LogDeErroDto>>> GetAllLogsAsync(CancellationToken cancellationToken)
    {
        var logsDeErro = await logDeErroService.GetAllAsync(cancellationToken);

        if (logsDeErro == null || !logsDeErro.Any())
            return NotFound(Result.Successful("Nenhum log de erro encontrado."));

        return Ok(Result.Successful(logsDeErro));
    }

    [HttpGet("{traceId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<LogDeErroDto>> GetLogByTraceIdAsync(string traceId, CancellationToken cancellationToken)
    {
        var log = await logDeErroService.GetLogByTraceIdAsync(traceId, cancellationToken);

        if (log == null)
            return NotFound(Result.Failure(null!, $"Log de erro com o TraceId {traceId} não encontrado."));

        return Ok(Result.Successful(log));
    }

    // Additional methods for creating, updating, and deleting logs can be added here
}
