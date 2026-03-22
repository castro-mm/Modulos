namespace Contas.Core.Objects.Requests;

public class UpdateRoleRequest
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
