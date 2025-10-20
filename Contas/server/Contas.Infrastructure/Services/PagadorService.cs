using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Interfaces.Repositories;
using Contas.Infrastructure.Services.Base;
using Contas.Infrastructure.Services.Interfaces;

namespace Contas.Infrastructure.Services;

public class PagadorService : Service<PagadorDto, Pagador>, IPagadorService
{
    private readonly IUnitOfWork _unitOfWork;

    public PagadorService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Implement any additional methods specific to Pagador if needed
    // For example, you might want to add methods for specific queries or operations    
}
