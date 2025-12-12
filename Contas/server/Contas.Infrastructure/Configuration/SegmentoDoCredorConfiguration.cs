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
        builder.Property(s => s.DataDeCriacao).IsRequired().HasColumnType("DATETIME2").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        builder.Property(s => s.DataDeAtualizacao).IsRequired().HasColumnType("DATETIME2").HasDefaultValueSql("GETDATE()");

        ConfigureRelationships(builder);
    }

    private void ConfigureTable(EntityTypeBuilder<SegmentoDoCredor> builder)
    {
        builder.ToTable(nameof(SegmentoDoCredor));
        builder.HasKey(s => s.Id);
    }

    private void ConfigureRelationships(EntityTypeBuilder<SegmentoDoCredor> builder)
    {
        builder.HasMany(s => s.Credores).WithOne(c => c.SegmentoDoCredor).HasForeignKey(c => c.SegmentoDoCredorId).OnDelete(DeleteBehavior.Restrict);
    }
}