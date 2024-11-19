using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dtos.Identity;

public class LoginDto
{
    [Required(ErrorMessage = "Usuário deve ser preenchido")]
    public required string UserName { get; set; } 

    [Required(ErrorMessage = "A Senha deve ser preenchida")]
    [DataType(DataType.Password)]
    public required string Password { get; set; } 
}