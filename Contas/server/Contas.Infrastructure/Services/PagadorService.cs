using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Interfaces.Repositories;
using Contas.Core.Interfaces.Services;
using Contas.Core.Interfaces.Services.Security;
using Contas.Infrastructure.Services.Base;

namespace Contas.Infrastructure.Services;

public class PagadorService : Service<PagadorDto, Pagador>, IPagadorService
{
    public PagadorService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : base(unitOfWork, currentUserService)
    {
    }

    // Implement any additional methods specific to Pagador if needed
    // For example, you might want to add methods for specific queries or operations    
}
