using Contas.Core.Entities;
using Contas.Core.Entities.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.Configuration.System;

public class ArquivoConfiguration : IEntityTypeConfiguration<Arquivo>
{
    public void Configure(EntityTypeBuilder<Arquivo> builder)
    {
        ConfigureTable(builder);

        builder.Property(a => a.Nome).IsRequired().HasMaxLength(255);
        builder.Property(a => a.Extensao).IsRequired().HasMaxLength(10);
        builder.Property(a => a.Tamanho).IsRequired();
        builder.Property(a => a.Tipo).IsRequired().HasMaxLength(100);
        builder.Property(a => a.DataDaUltimaModificacao).IsRequired();
        builder.Property(a => a.Dados).IsRequired();
        builder.Property(a => a.DataDeCriacao).IsRequired().HasColumnType("DATETIME2").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        builder.Property(a => a.DataDeAtualizacao).IsRequired().HasColumnType("DATETIME2").HasDefaultValueSql("GETDATE()");

        ConfigureRelationships(builder);
    }

    private void ConfigureTable(EntityTypeBuilder<Arquivo> builder)
    {
        builder.ToTable(nameof(Arquivo));
        builder.HasKey(a => a.Id);
    }

    private void ConfigureRelationships(EntityTypeBuilder<Arquivo> builder)
    {
        builder.HasOne(a => a.ArquivoDoRegistroDaConta).WithOne(a => a.Arquivo).HasForeignKey<ArquivoDoRegistroDaConta>(a => a.ArquivoId).OnDelete(DeleteBehavior.Cascade);
    }

}
