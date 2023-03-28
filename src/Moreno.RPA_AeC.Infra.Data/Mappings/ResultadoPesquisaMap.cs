using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moreno.RPA_AeC.Domain.Entities;

namespace Moreno.RPA_AeC.Infra.Data.Mappings;

[ExcludeFromCodeCoverage]
public class ResultadoPesquisaMap : IEntityTypeConfiguration<ResultadoPesquisa>
{
    public void Configure(EntityTypeBuilder<ResultadoPesquisa> builder)
    {
        builder.ToTable("Pesquisas_Resultados").HasKey(resultadoPesquisa => resultadoPesquisa.Id);

        builder.Property(resultadoPesquisa => resultadoPesquisa.Descricao).HasColumnName("varchar").HasMaxLength(1000);
        builder.Property(resultadoPesquisa => resultadoPesquisa.Titulo).HasColumnName("varchar").HasMaxLength(100);
        builder.Property(resultadoPesquisa => resultadoPesquisa.Area).HasColumnName("varchar").HasMaxLength(100);
        builder.Property(resultadoPesquisa => resultadoPesquisa.Autor).HasColumnName("varchar").HasMaxLength(100);

        builder.HasOne(resultadoPesquisa => resultadoPesquisa.Pesquisa)
            .WithMany(pesquisa => pesquisa.ResultadoPesquisas)
            .HasForeignKey(resultadoPesquisa => resultadoPesquisa.PesquisaId);
    }
}