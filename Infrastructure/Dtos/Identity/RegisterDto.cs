using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dtos.Identity;

public class RegisterDto
{
    [Required(ErrorMessage = "O Nome deve ser preenchido")]
    public required string Nome { get; set; } 
    
    [Required(ErrorMessage = "O Email deve ser preenchido")]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "O Usuário deve ser preenchido")]
    public required string UserName { get; set; } 
    
    [Required(ErrorMessage = "A Senha deve ser preenchida")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Devem haver no mínimo 08 caracteres")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    
    [Required(ErrorMessage = "A Confirmação da Senha deve ser preenchida")]
    [MinLength(8, ErrorMessage = "Devem haver no mínimo 08 caracteres")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "As senhas não conferem")]
    public required string ConfirmPassword { get; set; }

}
