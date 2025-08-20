using Contas.Api.Controllers.Base;
using Contas.Core.Dtos.System;
using Contas.Infrastructure.Services.Interfaces.System;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers.System;

public class TrilhaDeAuditoriaController(ITrilhaDeAuditoriaService service) : BaseApiController<TrilhaDeAuditoriaDto>(service)
{
    // Additional methods specific to TrilhaDeAuditoria can be added here
    override public Task<ActionResult> CreateAsync([FromBody] TrilhaDeAuditoriaDto dto, CancellationToken cancellationToken)
        => throw new InvalidOperationException("Este método não deve ser utilizado");
    override public Task<ActionResult> UpdateAsync(int id, [FromBody] TrilhaDeAuditoriaDto dto = null!, CancellationToken cancellationToken = default)
        => throw new InvalidOperationException("Este método não deve ser utilizado");
    override public Task<ActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        => throw new InvalidOperationException("Este método não deve ser utilizado");
}
