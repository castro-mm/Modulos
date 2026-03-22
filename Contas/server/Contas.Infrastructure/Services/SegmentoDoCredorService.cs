using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Interfaces;
using Contas.Core.Interfaces.Repositories;
using Contas.Core.Interfaces.Services;
using Contas.Infrastructure.Services.Base;

namespace Contas.Infrastructure.Services;

public class SegmentoDoCredorService : Service<SegmentoDoCredorDto, SegmentoDoCredor>, ISegmentoDoCredorService
{

    public SegmentoDoCredorService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    // Implement any additional methods specific to SegmentoDoCredor if needed
    // For example, you might want to add methods for specific queries or operations    
}