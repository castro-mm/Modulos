using Microsoft.EntityFrameworkCore;

namespace Contas.Infrastructure.Data;

public class ContasContext (DbContextOptions options) : DbContext(options)
{
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityConfiguration).Assembly);
    }
}
