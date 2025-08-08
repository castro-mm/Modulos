using Contas.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.Configuration;

public class CredorConfiguration : IEntityTypeConfiguration<Credor>
{
    public void Configure(EntityTypeBuilder<Credor> builder)
    {
        ConfigureTable(builder);

        builder.Property(c => c.RazaoSocial).IsRequired().HasMaxLength(200).HasColumnType("VARCHAR(200)");
        builder.Property(c => c.NomeFantasia).IsRequired().HasMaxLength(200).HasColumnType("VARCHAR(200)");
        builder.Property(c => c.CNPJ).IsRequired().HasColumnType("BIGINT");}

    private void ConfigureTable(EntityTypeBuilder<Credor> builder)
    {
        builder.ToTable(nameof(Credor));

        builder.HasKey(c => c.Id);
    }
}