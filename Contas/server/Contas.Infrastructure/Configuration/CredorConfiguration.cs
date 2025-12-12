using Contas.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contas.Infrastructure.Configuration;

public class CredorConfiguration : IEntityTypeConfiguration<Credor>
{
    public void Configure(EntityTypeBuilder<Credor> builder)
    {
        ConfigureTable(builder);

        builder.Property(c => c.SegmentoDoCredorId).IsRequired();
        builder.Property(c => c.RazaoSocial).IsRequired().HasMaxLength(200).HasColumnType("VARCHAR(200)");
        builder.Property(c => c.NomeFantasia).IsRequired().HasMaxLength(200).HasColumnType("VARCHAR(200)");
        builder.Property(c => c.CNPJ).IsRequired().HasColumnType("BIGINT");
        builder.Property(c => c.DataDeCriacao).IsRequired().HasColumnType("DATETIME2").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        builder.Property(c => c.DataDeAtualizacao).IsRequired().HasColumnType("DATETIME2").HasDefaultValueSql("GETDATE()");

        ConfigureRelationships(builder);
    }

    private void ConfigureTable(EntityTypeBuilder<Credor> builder)
    {
        builder.ToTable(nameof(Credor));
        builder.HasKey(c => c.Id);
    }

    private void ConfigureRelationships(EntityTypeBuilder<Credor> builder)
    {
        builder.HasOne(c => c.SegmentoDoCredor).WithMany(s => s.Credores).HasForeignKey(c => c.SegmentoDoCredorId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(c => c.RegistrosDaConta).WithOne(r => r.Credor).HasForeignKey(r => r.CredorId).OnDelete(DeleteBehavior.Restrict);
    }
}