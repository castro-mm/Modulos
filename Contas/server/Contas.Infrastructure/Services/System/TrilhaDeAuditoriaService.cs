using Contas.Core.Dtos.System;
using Contas.Core.Entities.System;
using Contas.Core.Interfaces.Repositories;
using Contas.Core.Interfaces.Services.Security;
using Contas.Core.Interfaces.Services.System;
using Contas.Infrastructure.Services.Base;

namespace Contas.Infrastructure.Services.System;

public class TrilhaDeAuditoriaService : Service<TrilhaDeAuditoriaDto, TrilhaDeAuditoria>, ITrilhaDeAuditoriaService
{
    private readonly IUnitOfWork _unitOfWork;

    public TrilhaDeAuditoriaService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : base(unitOfWork, currentUserService)
    {
        _unitOfWork = unitOfWork;
    }

    // Implement any additional methods specific to TrilhaDeAuditoria if needed
    // For example, you might want to add methods for specific queries or operations
}

