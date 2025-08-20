using Contas.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.Configuration;

public class ArquivoConfiguration : IEntityTypeConfiguration<Arquivo>
{
    public void Configure(EntityTypeBuilder<Arquivo> builder)
    {
        ConfigureTable(builder);

        builder.Property(x => x.RegistroDaContaId).IsRequired();
        builder.Property(x => x.Nome).IsRequired().HasMaxLength(200).HasColumnType("VARCHAR(200)");
        builder.Property(x => x.Extensao).IsRequired().HasMaxLength(10).HasColumnType("VARCHAR(10)");
        builder.Property(x => x.Tamanho).IsRequired().HasColumnType("BIGINT");
        builder.Property(x => x.Conteudo).IsRequired().HasColumnType("VARBINARY(MAX)");
        builder.Property(x => x.Tipo).IsRequired().HasConversion<string>();
    }

    private void ConfigureTable(EntityTypeBuilder<Arquivo> builder)
    {
        builder.ToTable(nameof(Arquivo));
        builder.HasKey(a => a.Id);
    }
}

