using Contas.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.Configuration;

public class SegmentoDoCredorConfiguration : IEntityTypeConfiguration<SegmentoDoCredor>
{
    public void Configure(EntityTypeBuilder<SegmentoDoCredor> builder)
    {
        ConfigureTable(builder);

        builder.Property(s => s.Nome).IsRequired().HasMaxLength(100).HasColumnType("VARCHAR(100)");
    }

    private void ConfigureTable(EntityTypeBuilder<SegmentoDoCredor> builder)
    {
        builder.ToTable(nameof(SegmentoDoCredor));

        builder.HasKey(s => s.Id);
    }
}