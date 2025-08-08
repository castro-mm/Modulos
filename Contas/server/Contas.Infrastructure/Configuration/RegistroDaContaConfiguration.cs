using Contas.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.Configuration;

public class RegistroDaContaConfiguration : IEntityTypeConfiguration<RegistroDaConta>
{
    public void Configure(EntityTypeBuilder<RegistroDaConta> builder)
    {
        ConfigureTable(builder);

        builder.Property(x => x.Valor).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(x => x.ValorDosJuros).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(x => x.ValorPago).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(x => x.ValorDoDesconto).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(x => x.CodigoDeBarras).HasMaxLength(100).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(x => x.BoletoBancario).HasColumnType("VARBINARY(MAX)").IsRequired();
        builder.Property(x => x.ComprovanteDePagamento).HasColumnType("VARBINARY(MAX)");
    }
    private void ConfigureTable(EntityTypeBuilder<RegistroDaConta> builder)
    {
        builder.ToTable(nameof(RegistroDaConta));
 
        builder.HasKey(r => r.Id);
    }
}