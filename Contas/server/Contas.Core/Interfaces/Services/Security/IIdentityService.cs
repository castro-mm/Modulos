using Contas.Core.Dtos.Security;
using Contas.Core.Objects;

namespace Contas.Core.Interfaces.Services.Security;

public interface IIdentityService
{
    public ValidationResult ValidationResult { get; set; }

    Task<int> RegisterUserAsync(string email, string password, string nomeCompleto);
    Task<string> LoginUserAsync(string email, string password);
    Task<bool> IsInRoleAsync(int userId, string roleName);

    Task<string> GenerateEmailConfirmationTokenAsync(int userId);
    Task<bool> ConfirmEmailAsync(string email, string token);

    Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
    Task<string> GeneratePasswordResetTokenAsync(string email);
    Task<bool> ResetPasswordAsync(string email, string token, string newPassword);

    Task<UserDto?> GetUserByIdAsync(int userId);
    Task<UserDto?> GetCurrentUserAsync(int userId);
    Task<List<UserDto>> GetAllUsersAsync();
    Task<bool> UpdateUserAsync(int userId, string nomeCompleto, string email, string role, bool isActive);
    Task<bool> UpdateUserRoleAsync(int userId, string newRole);
    Task<bool> DeactivateUserAsync(int userId);
    Task<bool> ActivateUserAsync(int userId);
    Task<bool> SetUserStatus(int userId, bool status);
    Task<bool> DeleteUserAsync(int userId);
    Task<string> AdminResetPasswordAsync(string email);

    // Foto de Perfil
    Task<bool> UpdateUserPhotoAsync(int userId, string fotoUrl);
    Task<bool> RemoveUserPhotoAsync(int userId);

    // Roles
    Task<List<RoleDto>> GetAllRolesAsync();
    Task<RoleDto?> GetRoleByIdAsync(int roleId);
    Task<int> CreateRoleAsync(string name, string criadoPor);
    Task<bool> UpdateRoleAsync(int roleId, string name);
    Task<bool> DeleteRoleAsync(int roleId);
}
