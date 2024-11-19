using Core.Entities.Identity;

namespace Infrastructure.Services.Interfaces;

public interface ITokenService
{
    /// <summary>
    /// Cria o Token de autenticação do usuário.
    /// </summary>
    /// <param name="user">Dados do usuário</param>
    /// <returns>O token com as informações da autenticação do usuário para uso nas políticas da aplicação</returns>
    Task<string> CreateToken(AppUser user);
}
