using AutoMapper;
using Core.Entities.Identity;
using Infrastructure.Dtos.Identity;
using Infrastructure.Services.Base;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AccountService(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService) : Service, IAccountService 
{
    public async Task Register(RegisterDto registerDto)
    {
        if (await userManager.Users.AnyAsync(x => StringComparer.CurrentCultureIgnoreCase.Compare(x.UserName, registerDto.UserName.ToLower()) == 0)) {
            ValidationResult.Add(statusCode: StatusCodes.Status400BadRequest, message: "Já existe cadastro com este usuário.");
            return;
        }
        
        if (await userManager.Users.AnyAsync(x => StringComparer.CurrentCultureIgnoreCase.Compare(x.Email, registerDto.Email.ToLower()) == 0)) {
            ValidationResult.Add(statusCode: StatusCodes.Status400BadRequest, message: "Já existe cadastro com este Email.");
            return;
        }

        var user = mapper.Map<AppUser>(registerDto);
        
        var identityResult = await userManager.CreateAsync(user, registerDto.Password);
        if(!identityResult.Succeeded) {
            ValidationResult.Add(statusCode: StatusCodes.Status400BadRequest, message: "Houve um erro ao cadastrar o usuário.", data: identityResult.Errors);
            return;
        }

        var roleResult = await userManager.AddToRoleAsync(user, "user");
        if(!roleResult.Succeeded) {
            ValidationResult.Add(statusCode: StatusCodes.Status400BadRequest, message: "Houve um erro ao cadastrar o usuário.", data: identityResult.Errors);
            return;
        }

        ValidationResult.Add(statusCode: StatusCodes.Status200OK, message: "Cadastro realizado com sucesso!", data: await MapToDtoAndCreateTokenAsync(user));
    }

    public async Task Login(LoginDto loginDto)
    {
        var user = userManager.Users.SingleOrDefault(x => StringComparer.CurrentCultureIgnoreCase.Compare(x.UserName, loginDto.UserName.ToLower()) == 0);
        if (user == null) {
            ValidationResult.Add(statusCode: StatusCodes.Status401Unauthorized, message: "Usuário não existe.");
            return;            
        }

        var isValidPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);
        if(!isValidPassword) {
            ValidationResult.Add(statusCode: StatusCodes.Status401Unauthorized, message: "Senha incorreta.");
            return;                        
        };

        ValidationResult.Add(statusCode: StatusCodes.Status200OK, data: await MapToDtoAndCreateTokenAsync(user));
    }

    private async Task<UserDto> MapToDtoAndCreateTokenAsync(AppUser user)
    {
        var userDto = mapper.Map<UserDto>(user);

        userDto.Token = await tokenService.CreateToken(user);

        return userDto;
    }
}
