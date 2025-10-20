using Contas.Core.Entities;
using Contas.Core.Entities.System;
using Contas.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Contas.Infrastructure.Data;

public class ContasContext : DbContext
{
    private readonly AuditSaveChangesInterceptor _auditInterceptor;

    public ContasContext(DbContextOptions options, AuditSaveChangesInterceptor auditInterceptor) : base(options)
    {
        _auditInterceptor = auditInterceptor;
    }

    #region [ System ]

    public DbSet<TrilhaDeAuditoria> TrilhasDeAuditoria { get; set; } 
    public DbSet<LogDeErro> LogsDeErro { get; set; }

    #endregion

    #region [ Entities ]

    public DbSet<Pagador> Pagadores { get; set; }
    public DbSet<Credor> Credores { get; set; }
    public DbSet<SegmentoDoCredor> SegmentosDoCredor { get; set; }
    public DbSet<RegistroDaConta> RegistrosDaConta { get; set; }
    public DbSet<Arquivo> Arquivos { get; set; }

    #endregion

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContasContext).Assembly);
    }

    override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(_auditInterceptor);
    }
}
