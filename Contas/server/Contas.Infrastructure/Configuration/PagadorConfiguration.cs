using Contas.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.Configuration;

public class PagadorConfiguration : IEntityTypeConfiguration<Pagador>
{
    public void Configure(EntityTypeBuilder<Pagador> builder)
    {
        ConfigureTable(builder);

        builder.Property(p => p.Nome).IsRequired().HasMaxLength(200).HasColumnType("VARCHAR(200)");
        builder.Property(p => p.Email).IsRequired().HasMaxLength(200).HasColumnType("VARCHAR(200)");
        builder.Property(p => p.CPF).IsRequired().HasColumnType("BIGINT");
    }

    private void ConfigureTable(EntityTypeBuilder<Pagador> builder)
    {
        builder.ToTable(nameof(Pagador));

        builder.HasKey(p => p.Id);
    }
}
