using Contas.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contas.Infrastructure.Data;

public class ContasContext (DbContextOptions options) : DbContext(options)
{
    public DbSet<Pagador> Pagadores { get; set; }
    public DbSet<Credor> Credores { get; set; }
    public DbSet<SegmentoDoCredor> SegmentosDoCredor { get; set; }
    public DbSet<RegistroDaConta> RegistrosDaConta { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContasContext).Assembly);
    }
}
