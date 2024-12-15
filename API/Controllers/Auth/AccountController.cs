using API.Base.Controllers;
using Infrastructure.Dtos.Identity;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Auth;

public class AccountController(IAccountService accountService) : ApiBaseController
{
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        await accountService.Register(registerDto);

        return GetResult(accountService.ValidationResult);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        await accountService.Login(loginDto);

        return GetResult(accountService.ValidationResult);
    }
}
