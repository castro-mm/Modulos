using Contas.Core.Dtos;
using Contas.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Contas.Infrastructure.Services.Interfaces;

public interface IRegistroDaContaService : IService<RegistroDaContaDto, RegistroDaConta>
{
}
