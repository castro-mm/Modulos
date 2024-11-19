using Core.Entities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data;

public static class SeederExtensions
{
    public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        try
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
            var context = scope.ServiceProvider.GetRequiredService<ModuloContext>();

            await context.Database.MigrateAsync();

            // verifica se já existe algum usuário registrado
            if (!await userManager.Users.AnyAsync())
            {
                // caso não exista, define as informaçoes do usuário.
                var user = new AppUser
                {
                    Nome = "Marcelo Moura de Castro",
                    Email = "castro.mm@yahoo.com",
                    Cpf = 94388989134,
                    DataDeNascimento = new DateTime(1980, 09, 21),
                    DataDoCadastro = DateTime.Now,
                    DataDoUltimoAcesso = DateTime.Now,
                    UserName = "adm.castro"
                };

                // define a senha e cria o usuário.
                var userCreateResult = await userManager.CreateAsync(user, "Pa$$w0rd");
                if (!userCreateResult.Succeeded)
                    throw new ArgumentException("Houve uma falha durante a criação do usuário");

                // define os perfis da aplicação (Cada Módulo precisará de um perfil especifico para ele)
                var roles = new List<AppRole>
                {
                    new() { Name = "admin" },
                    new() { Name = "user" }
                };
                foreach (var role in roles) await roleManager.CreateAsync(role);

                // define o perfil do usuário no sistema
                var roleCreateResult = await userManager.AddToRoleAsync(user, "admin");
                if (!roleCreateResult.Succeeded)
                    throw new ArgumentException("Houve uma falha durante a criação do perfil do usuário");
            }
        }
        catch (Exception)
        {
            throw;
        }

        return app;
    }
}
