using Contas.Api.Controllers.Base;
using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos.System;
using Contas.Core.Entities.System;
using Contas.Core.Interfaces.Services.System;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers.System;

public class TrilhaDeAuditoriaController(ITrilhaDeAuditoriaService service, ITrilhaDeAuditoriaValidator validator) : BaseApiController<TrilhaDeAuditoriaDto, TrilhaDeAuditoria>(service, validator)
{

    // Additional methods specific to TrilhaDeAuditoria can be added here
    override public Task<ActionResult> CreateAsync([FromBody] TrilhaDeAuditoriaDto dto, CancellationToken cancellationToken)
        => throw new InvalidOperationException("Esta chamada é proibida");
    override public Task<ActionResult> UpdateAsync(int id, [FromBody] TrilhaDeAuditoriaDto dto = null!, CancellationToken cancellationToken = default)
        => throw new InvalidOperationException("Esta chamada é proibida");
    override public Task<ActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        => throw new InvalidOperationException("Esta chamada é proibida");
}
