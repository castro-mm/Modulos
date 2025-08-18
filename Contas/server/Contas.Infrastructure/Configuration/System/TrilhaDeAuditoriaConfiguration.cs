using Contas.Core.Entities.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.Configuration.System;

public class TrilhaDeAuditoriaConfiguration : IEntityTypeConfiguration<TrilhaDeAuditoria>
{
    public void Configure(EntityTypeBuilder<TrilhaDeAuditoria> builder)
    {
        ConfigureTable(builder);

        builder.Property(t => t.Entidade).IsRequired().HasMaxLength(50).HasColumnType("VARCHAR(50)");
        builder.Property(t => t.Metodo).IsRequired().HasMaxLength(50).HasColumnType("VARCHAR(50)");
        builder.Property(t => t.Caminho).IsRequired().HasMaxLength(200).HasColumnType("VARCHAR(200)");
        builder.Property(t => t.Operacao).IsRequired().HasMaxLength(20).HasColumnType("VARCHAR(20)");
        builder.Property(t => t.ValoresAntigos).HasColumnType("VARCHAR(MAX)");
        builder.Property(t => t.ValoresNovos).HasColumnType("VARCHAR(MAX)");
        builder.Property(t => t.DataDeCriacao).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        builder.Property(t => t.DataDeAtualizacao).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        builder.Property(t => t.Ip).IsRequired().HasColumnType("VARCHAR(45)");
        builder.Property(t => t.Navegador).IsRequired().HasColumnType("VARCHAR(200)");
        builder.Property(t => t.Usuario).IsRequired().HasMaxLength(100).HasColumnType("VARCHAR(100)");
        builder.Property(t => t.TraceId).IsRequired().HasMaxLength(36).HasColumnType("VARCHAR(36)");
        builder.Property(t => t.Hash).IsRequired().HasMaxLength(32).HasColumnType("VARCHAR(32)");
    }

    private void ConfigureTable(EntityTypeBuilder<TrilhaDeAuditoria> builder)
    {
        builder.ToTable(nameof(TrilhaDeAuditoria));

        builder.HasKey(t => t.Id);
    }
}
