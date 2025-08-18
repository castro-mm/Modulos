using Contas.Core.Dtos.System;
using Contas.Core.Entities.System;
using Contas.Core.Interfaces.Repositories;
using Contas.Infrastructure.Services.Base;
using Contas.Infrastructure.Services.Interfaces.System;

namespace Contas.Infrastructure.Services.System;

public class TrilhaDeAuditoriaService(IUnitOfWork unitOfWork) : Service<TrilhaDeAuditoriaDto, TrilhaDeAuditoria>(unitOfWork), ITrilhaDeAuditoriaService
{
    // Implement any additional methods specific to TrilhaDeAuditoria if needed
    // For example, you might want to add methods for specific queries or operations
}

