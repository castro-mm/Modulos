namespace Infrastructure.Dtos.Identity;

public class UserDto
{
    public int Id { get; set; }
    public required string Nome { get; set; } 
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public required string Token { get; set; }
}
