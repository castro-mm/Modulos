using Contas.Core.Entities.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.Configuration.System;

public class LogDeErroConfiguration : IEntityTypeConfiguration<LogDeErro>
{
    public void Configure(EntityTypeBuilder<LogDeErro> builder)
    {
        ConfigureTable(builder);

        builder.Property(l => l.Mensagem).IsRequired().HasColumnType("VARCHAR(MAX)");
        builder.Property(l => l.Detalhes).IsRequired().HasColumnType("VARCHAR(MAX)");
        builder.Property(l => l.Metodo).IsRequired().HasColumnType("VARCHAR(MAX)");
        builder.Property(l => l.Caminho).IsRequired().HasColumnType("VARCHAR(MAX)");
        builder.Property(l => l.Ip).IsRequired().HasColumnType("VARCHAR(45)");
        builder.Property(l => l.Navegador).IsRequired().HasColumnType("VARCHAR(200)");
        builder.Property(l => l.DataDeCriacao).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        builder.Property(l => l.DataDeAtualizacao).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        builder.Property(l => l.Usuario).IsRequired().HasMaxLength(100).HasColumnType("VARCHAR(100)");
        builder.Property(l => l.TraceId).IsRequired().HasMaxLength(36).HasColumnType("VARCHAR(36)");
        builder.Property(l => l.Hash).IsRequired().HasMaxLength(32).HasColumnType("VARCHAR(32)");
    }

    private void ConfigureTable(EntityTypeBuilder<LogDeErro> builder)
    {
        builder.ToTable(nameof(LogDeErro));

        builder.HasKey(l => l.Id);
        builder.HasIndex(l => l.TraceId).IsUnique();
    }
}
