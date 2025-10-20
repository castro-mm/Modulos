using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Interfaces.Repositories;
using Contas.Infrastructure.Services.Base;
using Contas.Infrastructure.Services.Interfaces;

namespace Contas.Infrastructure.Services;

public class ArquivoService : Service<ArquivoDto, Arquivo>, IArquivoService
{
    private readonly IUnitOfWork _unitOfWork;

    public ArquivoService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Implement any additional methods specific to Arquivo if needed
    // For example, you might want to add methods for specific queries or operations    
}
