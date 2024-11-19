using System.Security.Authentication;
using System.Security.Claims;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static async Task<AppUser> GetUserByEmail(this UserManager<AppUser> userManager, ClaimsPrincipal user) 
        => await userManager.Users.FirstOrDefaultAsync(x => x.Email == GetEmail(user)) ?? throw new AuthenticationException("Email claim not found");

    public static string GetEmail(this ClaimsPrincipal user) 
        => user.FindFirstValue(ClaimTypes.Email) ?? throw new AuthenticationException("Email claim not found");
}
