using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Interfaces.Repositories;
using Contas.Core.Interfaces.Services;
using Contas.Infrastructure.Services.Base;

namespace Contas.Infrastructure.Services;

public class CredorService : Service<CredorDto, Credor>, ICredorService
{
    private readonly IUnitOfWork _unitOfWork;

    public CredorService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Implement any additional methods specific to Credor if needed
    // For example, you might want to add methods for specific queries or operations
}
