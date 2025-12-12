using Contas.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.Configuration;

public class RegistroDaContaConfiguration : IEntityTypeConfiguration<RegistroDaConta>
{
    public void Configure(EntityTypeBuilder<RegistroDaConta> builder)
    {
        ConfigureTable(builder);

        builder.Property(x => x.CredorId).IsRequired();
        builder.Property(x => x.PagadorId).IsRequired();
        builder.Property(x => x.Mes).IsRequired();
        builder.Property(x => x.Ano).IsRequired();
        builder.Property(x => x.DataDeVencimento).HasColumnType("DATETIME").IsRequired();
        builder.Property(x => x.DataDePagamento).HasColumnType("DATETIME2");
        builder.Property(x => x.Valor).HasColumnType("DECIMAL(18,2)").IsRequired();
        builder.Property(x => x.ValorDosJuros).HasColumnType("DECIMAL(18,2)");
        builder.Property(x => x.ValorTotal).HasColumnType("DECIMAL(18,2)").IsRequired();
        builder.Property(x => x.ValorDoDesconto).HasColumnType("DECIMAL(18,2)");
        builder.Property(x => x.CodigoDeBarras).HasMaxLength(100).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(x => x.Observacoes).HasMaxLength(500).HasColumnType("VARCHAR(500)");
        builder.Property(x => x.DataDeCriacao).IsRequired().HasColumnType("DATETIME2").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        builder.Property(x => x.DataDeAtualizacao).IsRequired().HasColumnType("DATETIME2").HasDefaultValueSql("GETDATE()");

        this.ConfigureRelationships(builder);
    }
    private void ConfigureTable(EntityTypeBuilder<RegistroDaConta> builder)
    {
        builder.ToTable(nameof(RegistroDaConta));
 
        builder.HasKey(r => r.Id);
    }

    private void ConfigureRelationships(EntityTypeBuilder<RegistroDaConta> builder)
    {
        builder.HasOne(r => r.Credor).WithMany(c => c.RegistrosDaConta).HasForeignKey(r => r.CredorId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(r => r.Pagador).WithMany(p => p.RegistrosDaConta).HasForeignKey(r => r.PagadorId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(r => r.ArquivosDoRegistroDaConta).WithOne(ar => ar.RegistroDaConta).HasForeignKey(ar => ar.RegistroDaContaId).OnDelete(DeleteBehavior.Cascade);
    }
}