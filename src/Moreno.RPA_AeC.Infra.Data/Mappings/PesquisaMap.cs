using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moreno.RPA_AeC.Domain.Entities;

namespace Moreno.RPA_AeC.Infra.Data.Mappings;

[ExcludeFromCodeCoverage]
public class PesquisaMap : IEntityTypeConfiguration<Pesquisa>
{
    public void Configure(EntityTypeBuilder<Pesquisa> builder)
    {
        builder.ToTable("Pesquisas");

        builder.Property(pesquisa => pesquisa.Termo).HasColumnType("varchar").HasMaxLength(100).IsRequired();
        builder.Property(pesquisa => pesquisa.DataCadastro).IsRequired();
    }
}
