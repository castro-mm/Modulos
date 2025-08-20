using Contas.Core.Entities;
using Contas.Core.Entities.System;
using Contas.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Contas.Infrastructure.Data;

public class ContasContext(DbContextOptions options, AuditSaveChangesInterceptor auditInterceptor) : DbContext(options)
{
    public DbSet<TrilhaDeAuditoria> TrilhasDeAuditoria { get; set; } 
    public DbSet<LogDeErro> LogsDeErro { get; set; }

    public DbSet<Pagador> Pagadores { get; set; }
    public DbSet<Credor> Credores { get; set; }
    public DbSet<SegmentoDoCredor> SegmentosDoCredor { get; set; }
    public DbSet<RegistroDaConta> RegistrosDaConta { get; set; }
    public DbSet<Arquivo> Arquivos { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContasContext).Assembly);
    }

    override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(auditInterceptor);
    }
}
