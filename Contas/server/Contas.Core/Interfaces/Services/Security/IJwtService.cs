using Contas.Core.Entities.System.Security;

namespace Contas.Core.Interfaces.Services.Security;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(ApplicationUser user);
}
