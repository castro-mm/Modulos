using Contas.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.Configuration;

public class ArquivoDoRegistroDaContaConfiguration : IEntityTypeConfiguration<ArquivoDoRegistroDaConta>
{
    public void Configure(EntityTypeBuilder<ArquivoDoRegistroDaConta> builder)
    {
        ConfigureTable(builder);

        builder.Property(ar => ar.RegistroDaContaId).IsRequired();
        builder.Property(ar => ar.ArquivoId).IsRequired();
        builder.Property(ar => ar.ModalidadeDoArquivo).IsRequired();
        builder.Property(ar => ar.DataDeCriacao).IsRequired().HasColumnType("DATETIME2").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        builder.Property(ar => ar.DataDeAtualizacao).IsRequired().HasColumnType("DATETIME2").HasDefaultValueSql("GETDATE()");

        ConfigureRelationships(builder);
    }

    private void ConfigureTable(EntityTypeBuilder<ArquivoDoRegistroDaConta> builder)
    {
        builder.ToTable(nameof(ArquivoDoRegistroDaConta));
        builder.HasKey(ar => ar.Id);
    }

    private void ConfigureRelationships(EntityTypeBuilder<ArquivoDoRegistroDaConta> builder)
    {
        builder.HasOne(ar => ar.RegistroDaConta).WithMany(rc => rc.ArquivosDoRegistroDaConta).HasForeignKey(ar => ar.RegistroDaContaId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(ar => ar.Arquivo).WithOne(ar => ar.ArquivoDoRegistroDaConta).HasForeignKey<ArquivoDoRegistroDaConta>(ar => ar.ArquivoId).OnDelete(DeleteBehavior.Cascade);
    }
}
