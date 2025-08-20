using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Interfaces.Repositories;
using Contas.Infrastructure.Services.Base;
using Contas.Infrastructure.Services.Interfaces;

namespace Contas.Infrastructure.Services;

public class CredorService(IUnitOfWork unitOfWork) : Service<CredorDto, Credor>(unitOfWork), ICredorService
{
}
