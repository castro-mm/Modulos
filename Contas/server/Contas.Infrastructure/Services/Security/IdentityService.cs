using Contas.Core.Dtos.Security;
using Contas.Core.Entities.System.Security;
using Contas.Core.Interfaces.Services.Security;
using Contas.Core.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Contas.Infrastructure.Services.Security;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IJwtService _jwtService;

    public ValidationResult ValidationResult { get; set; } = new();

    public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtService = jwtService;
    }

    #region [ Autenticacao ]

    public async Task<int> RegisterUserAsync(string email, string password, string nomeCompleto)
    {        
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            NomeCompleto = nomeCompleto
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {   
            foreach(var error in result.Errors)
            {
                var message = new Dictionary<string, string>
                {
                    { "DuplicateUserName", "E-mail já cadastrado." },
                    { "PasswordTooShort", "A senha deve conter pelo menos 6 caracteres." },
                    { "PasswordRequiresNonAlphanumeric", "A senha deve conter pelo menos um caractere especial." },
                    { "PasswordRequiresDigit", "A senha deve conter pelo menos um número." },
                    { "PasswordRequiresUpper", "A senha deve conter pelo menos uma letra maiúscula." },
                    { "PasswordRequiresLower", "A senha deve conter pelo menos uma letra minúscula." }
                };

                ValidationResult.AddError(error.Code, message[error.Code] ?? "Erro desconhecido");
            }
            return 0;
        }

        await _userManager.AddToRoleAsync(user, "User");

        return user.Id;
    }

    public async Task<string> LoginUserAsync(string email, string password)
    {
        var user = await GetByUserEmailAsync(email);
        if (user == null) return string.Empty;

        if (!user!.IsActive)
            ValidationResult.AddError("USUARIO_INATIVO", "Usuário está inativo");

        if (!user!.EmailConfirmed)
            ValidationResult.AddError("EMAIL_NAO_CONFIRMADO", "E-mail não confirmado. Verifique sua caixa de entrada.");

        if (user!.MustChangePassword)
            ValidationResult.AddError("SENHA_RESETADA", "Sua senha foi redefinida pelo administrador. Verifique seu e-mail para criar uma nova senha.");

        if (!ValidationResult.IsValid) return string.Empty;

        var result = await _userManager.CheckPasswordAsync(user!, password);
        if (!result)
            ValidationResult.AddError("CREDENCIAIS_INVALIDAS", "Senha incorreta");

        if (!ValidationResult.IsValid) return string.Empty;

        var token = await _jwtService.GenerateTokenAsync(user!);

        return token;
    }

    public async Task<bool> IsInRoleAsync(int userId, string roleName)
    {
        var user = await GetByUserIdAsync(userId);
        return user != null && await _userManager.IsInRoleAsync(user, roleName);
    }

    #endregion

    #region [ Confirmação de E-mail ]

    public async Task<string> GenerateEmailConfirmationTokenAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return string.Empty;

        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<bool> ConfirmEmailAsync(string email, string token)
    {
        var user = await GetByUserEmailAsync(email);
        if (user == null) return false;

        if (user.EmailConfirmed)
        {
            ValidationResult.AddError("EMAIL_JA_CONFIRMADO", "Este e-mail já foi confirmado.");
            return false;
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
        {
            ValidationResult.AddError("TOKEN_INVALIDO", "O link de confirmação é inválido ou expirou.");
            return false;
        }

        return true;
    }

    #endregion

    #region [ Senha ]

    public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
    {
        var user = await GetByUserIdAsync(userId);
        if (user == null) return false;

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if (!result.Succeeded)
        {
            foreach(var error in result.Errors)
            {
                var message = new Dictionary<string, string>
                {
                    { "PasswordMismatch", "A senha atual está incorreta." },
                    { "PasswordTooShort", "A nova senha deve conter pelo menos 6 caracteres." },
                    { "PasswordRequiresNonAlphanumeric", "A nova senha deve conter pelo menos um caractere especial." },
                    { "PasswordRequiresDigit", "A nova senha deve conter pelo menos um número." },
                    { "PasswordRequiresUpper", "A nova senha deve conter pelo menos uma letra maiúscula." },
                    { "PasswordRequiresLower", "A nova senha deve conter pelo menos uma letra minúscula." }
                };

                ValidationResult.AddError(error.Code, message[error.Code]);
            }            
        }

        if (!ValidationResult.IsValid) return false;

        user.DataDeAtualizacao = DateTime.Now;    
        await _userManager.UpdateAsync(user);

        return true;
    }

    public async Task<string> GeneratePasswordResetTokenAsync(string email)
    {
        var user = await GetByUserEmailAsync(email);

        return user == null || !user.IsActive
            ? string.Empty
            : await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await GetByUserEmailAsync(email);
        if (user == null) return false;
        
        if (!ValidationResult.IsValid) return false;

        var result = await _userManager.ResetPasswordAsync(user!, token, newPassword);

        if (!result.Succeeded) {
            foreach(var error in result.Errors) {
                var message = new Dictionary<string, string>
                {
                    { "InvalidToken", "O token de redefinição de senha é inválido ou expirou." },
                    { "PasswordTooShort", "A nova senha deve conter pelo menos 6 caracteres." },
                    { "PasswordRequiresNonAlphanumeric", "A nova senha deve conter pelo menos um caractere especial." },
                    { "PasswordRequiresDigit", "A nova senha deve conter pelo menos um número." },
                    { "PasswordRequiresUpper", "A nova senha deve conter pelo menos uma letra maiúscula." },
                    { "PasswordRequiresLower", "A nova senha deve conter pelo menos uma letra minúscula." }
                };

                ValidationResult.AddError(error.Code, message[error.Code]);
            }
        }

        if (!ValidationResult.IsValid) return false;

        user!.MustChangePassword = false;
        user!.DataDeAtualizacao = DateTime.Now;
        await _userManager.UpdateAsync(user!);

        return true;
    }

    public async Task<UserDto?> GetUserByIdAsync(int userId)
    {
        return await GetCurrentUserAsync(userId);
    }

    #endregion

    #region [ Gestão de Usuários (Admin) ]

    public async Task<UserDto?> GetCurrentUserAsync(int userId)
    {
        var user = await GetByUserIdAsync(userId);
        if (user == null) return null;

        var userRoles = await _userManager.GetRolesAsync(user!);

        return new UserDto {
            Id = user!.Id,
            NomeCompleto = user.NomeCompleto,
            Email = user.Email!,
            Role = userRoles.FirstOrDefault() ?? "User",
            IsActive = user.IsActive,
            EmailConfirmed = user.EmailConfirmed,
            MustChangePassword = user.MustChangePassword,
            FotoUrl = user.FotoUrl,
            DataDeCriacao = user.DataDeCriacao,
            DataDeAtualizacao = user.DataDeAtualizacao            
        };
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();

        return users.Select(u => new UserDto
        {
            Id = u.Id,
            NomeCompleto = u.NomeCompleto,
            Email = u.Email!,
            Role = _userManager.GetRolesAsync(u).Result.FirstOrDefault() ?? "User",
            IsActive = u.IsActive,
            EmailConfirmed = u.EmailConfirmed,
            MustChangePassword = u.MustChangePassword,
            FotoUrl = u.FotoUrl,
            DataDeCriacao = u.DataDeCriacao,
            DataDeAtualizacao = u.DataDeAtualizacao
        }).ToList();
    }

    public async Task<bool> UpdateUserAsync(int userId, string nomeCompleto, string email, string role, bool isActive)
    {
        var user = await GetByUserIdAsync(userId);
        if (user == null) return false;

        // Atualizar dados básicos
        user.NomeCompleto = nomeCompleto;
        user.DataDeAtualizacao = DateTime.Now;
        user.IsActive = isActive;

        // Se o email mudou, atualizar
        if (!string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase))
        {
            user.Email = email;
            user.UserName = email;
            user.NormalizedEmail = email.ToUpperInvariant();
            user.NormalizedUserName = email.ToUpperInvariant();
        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ValidationResult.AddError(error.Code, error.Description);
            return false;
        }

        // Atualizar role se necessário
        var currentRoles = await _userManager.GetRolesAsync(user);
        var currentRole = currentRoles.FirstOrDefault() ?? "User";

        if (!string.Equals(currentRole, role, StringComparison.OrdinalIgnoreCase))
        {
            await _userManager.RemoveFromRoleAsync(user, currentRole);
            var roleResult = await _userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
            {
                ValidationResult.AddError("ROLE_NAO_ATUALIZADA", "Erro ao atualizar o perfil do usuário.");
                return false;
            }
        }

        return true;
    }

    public async Task<bool> UpdateUserRoleAsync(int userId, string newRole)
    {
        var user = await GetByUserIdAsync(userId);
        if (user == null) return false;

        var roles = await _userManager.GetRolesAsync(user!);

        var result = await _userManager.RemoveFromRoleAsync(user!, roles.FirstOrDefault() ?? "User");
        if (!result.Succeeded)
            ValidationResult.AddError("ROLE_NAO_EXCLUIDA", "Erro ao excluir o perfil existente.");

        if (!ValidationResult.IsValid) return false;

        result = await _userManager.AddToRoleAsync(user!, newRole);
        
        if (!result.Succeeded)
            ValidationResult.AddError("ROLE_NAO_INSERIDA", "Erro ao atualizar o perfil.");

        if (!ValidationResult.IsValid) return false;

        return true;
    }

    public async Task<bool> DeactivateUserAsync(int userId)
    {
        var user = await GetByUserIdAsync(userId);
        if (user == null) return false;

        user!.IsActive = false;
        user!.DataDeAtualizacao = DateTime.Now;

        await _userManager.UpdateAsync(user!);

        return true;
    }

    public async Task<bool> ActivateUserAsync(int userId)
    {
        var user = await GetByUserIdAsync(userId);
        if (user == null) return false;

        user!.IsActive = true;
        user!.DataDeAtualizacao = DateTime.Now;

        await _userManager.UpdateAsync(user!);

        return true;
    }

    public async Task<bool> SetUserStatus(int userId, bool status)
    {
        var user = await GetByUserIdAsync(userId);
        if (user == null) return false;

        user.IsActive = status;
        user.DataDeAtualizacao = DateTime.Now;
    
        await _userManager.UpdateAsync(user);
        
        return true;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await GetByUserIdAsync(userId);
        if (user == null) return false;

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ValidationResult.AddError(error.Code, error.Description);
            return false;
        }

        return true;
    }

    public async Task<string> AdminResetPasswordAsync(string email)
    {
        var user = await GetByUserEmailAsync(email);
        if (user == null) return string.Empty;

        user.MustChangePassword = true;
        user.DataDeAtualizacao = DateTime.Now;
        await _userManager.UpdateAsync(user);

        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<bool> UpdateUserPhotoAsync(int userId, string fotoUrl)
    {
        var user = await GetByUserIdAsync(userId);
        if (user == null) return false;

        user.FotoUrl = fotoUrl;
        user.DataDeAtualizacao = DateTime.Now;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ValidationResult.AddError(error.Code, error.Description);
            return false;
        }

        return true;
    }

    public async Task<bool> RemoveUserPhotoAsync(int userId)
    {
        var user = await GetByUserIdAsync(userId);
        if (user == null) return false;

        user.FotoUrl = null;
        user.DataDeAtualizacao = DateTime.Now;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ValidationResult.AddError(error.Code, error.Description);
            return false;
        }

        return true;
    }

    #endregion

    #region [ Gestão de Roles (Admin) ]

    public async Task<List<RoleDto>> GetAllRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return roles.Select(r => new RoleDto { 
            Id = r.Id, 
            Name = r.Name!,
            CriadoPor = r.CriadoPor,
            DataDeCriacao = r.DataDeCriacao,
            DataDeAtualizacao = r.DataDeAtualizacao
        }).ToList();
    }

    public async Task<RoleDto?> GetRoleByIdAsync(int roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        if (role == null)
        {
            ValidationResult.AddError("ROLE_NAO_ENCONTRADA", "Perfil não encontrado.");
            return null;
        }

        return new RoleDto { 
            Id = role.Id, 
            Name = role.Name!,
            CriadoPor = role.CriadoPor,
            DataDeCriacao = role.DataDeCriacao,
            DataDeAtualizacao = role.DataDeAtualizacao
        };
    }

    public async Task<int> CreateRoleAsync(string name, string criadoPor)
    {
        if (await _roleManager.RoleExistsAsync(name))
        {
            ValidationResult.AddError("ROLE_JA_EXISTE", "Já existe um perfil com este nome.");
            return 0;
        }

        var role = new ApplicationRole 
        { 
            Name = name,
            CriadoPor = criadoPor,
            DataDeCriacao = DateTime.Now,
            DataDeAtualizacao = DateTime.Now
        };
        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ValidationResult.AddError(error.Code, error.Description);
            return 0;
        }

        return role.Id;
    }

    public async Task<bool> UpdateRoleAsync(int roleId, string name)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        if (role == null)
        {
            ValidationResult.AddError("ROLE_NAO_ENCONTRADA", "Perfil não encontrado.");
            return false;
        }

        // Verificar se já existe outro perfil com o mesmo nome
        var existing = await _roleManager.FindByNameAsync(name);
        if (existing != null && existing.Id != roleId)
        {
            ValidationResult.AddError("ROLE_JA_EXISTE", "Já existe um perfil com este nome.");
            return false;
        }

        role.Name = name;
        role.DataDeAtualizacao = DateTime.Now;
        var result = await _roleManager.UpdateAsync(role);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ValidationResult.AddError(error.Code, error.Description);
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteRoleAsync(int roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        if (role == null)
        {
            ValidationResult.AddError("ROLE_NAO_ENCONTRADA", "Perfil não encontrado.");
            return false;
        }

        // Verificar se há usuários associados a este perfil
        var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name!);
        if (usersInRole.Any())
        {
            ValidationResult.AddError("ROLE_EM_USO", $"Não é possível excluir o perfil '{role.Name}' pois há {usersInRole.Count} usuário(s) associado(s).");
            return false;
        }

        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ValidationResult.AddError(error.Code, error.Description);
            return false;
        }

        return true;
    }

    #endregion

    #region [ Metodos Privados ]

    private async Task<ApplicationUser?> GetByUserIdAsync(int userId) 
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) 
            ValidationResult.AddError("USUARIO_NAO_ENCONTRADO", "Usuário não encontrado");
        
        return user;
    }

    private async Task<ApplicationUser?> GetByUserEmailAsync(string email) 
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) 
            ValidationResult.AddError("USUARIO_NAO_ENCONTRADO", "E-mail não encontrado.");
        
        return user;
    }

    #endregion
}
