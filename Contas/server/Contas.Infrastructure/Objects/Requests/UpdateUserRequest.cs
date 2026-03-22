namespace Contas.Core.Objects.Requests;

public class UpdateUserRequest
{
    public required string NomeCompleto { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
    public bool IsActive { get; set; }
}
