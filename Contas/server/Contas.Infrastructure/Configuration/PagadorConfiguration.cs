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
        builder.Property(p => p.DataDeCriacao).IsRequired().HasColumnType("DATETIME2").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        builder.Property(p => p.DataDeAtualizacao).IsRequired().HasColumnType("DATETIME2").HasDefaultValueSql("GETDATE()");

        ConfigureRelationships(builder);
    }

    private void ConfigureTable(EntityTypeBuilder<Pagador> builder)
    {
        builder.ToTable(nameof(Pagador));
        builder.HasKey(p => p.Id);
    }

    private void ConfigureRelationships(EntityTypeBuilder<Pagador> builder)
    {
        // Configure relationships here if needed
        builder.HasMany(p => p.RegistrosDaConta).WithOne(c => c.Pagador).HasForeignKey(c => c.PagadorId).OnDelete(DeleteBehavior.Restrict);
    }
}
