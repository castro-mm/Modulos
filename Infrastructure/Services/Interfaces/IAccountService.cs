using Infrastructure.Dtos.Identity;
using Infrastructure.Services.Base;

namespace Infrastructure.Services.Interfaces;

/// <summary>
/// Interface que define as funcionalidades de registro e autenticação da aplicação, bem como suas regras de negócio.
/// </summary>
public interface IAccountService : IService
{
    /// <summary>
    /// Método que realiza o registro do usuário na aplicação.
    /// </summary>
    /// <param name="registerDto">Representa a dto com os parâmetros do registro do usuário.</param>
    /// <returns>Os dados do usuário registrado.</returns>
    Task Register(RegisterDto registerDto);

    /// <summary>
    /// Método que realiza a autenticação do usuário na aplicação
    /// </summary>
    /// <param name="loginDto">Representa a dto com os parâmetros de autenticação do usuário.</param>
    /// <returns>Os dados do usuário autenticado.</returns>
    Task Login(LoginDto loginDto);
}