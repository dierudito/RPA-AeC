using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moreno.RPA_AeC.Domain.Entities;

namespace Moreno.RPA_AeC.Infra.Data.Mappings;

[ExcludeFromCodeCoverage]
public class ResultadoPesquisaMap : IEntityTypeConfiguration<ResultadoPesquisa>
{
    public void Configure(EntityTypeBuilder<ResultadoPesquisa> builder)
    {
        builder.ToTable("Pesquisas_Resultados");

        builder.Property(resultadoPesquisa => resultadoPesquisa.Descricao).HasColumnType("varchar").HasMaxLength(1000);
        builder.Property(resultadoPesquisa => resultadoPesquisa.Titulo).HasColumnType("varchar").HasMaxLength(100);
        builder.Property(resultadoPesquisa => resultadoPesquisa.Area).HasColumnType("varchar").HasMaxLength(100);
        builder.Property(resultadoPesquisa => resultadoPesquisa.Autor).HasColumnType("varchar").HasMaxLength(100);
        builder.Property(resultadoPesquisa => resultadoPesquisa.CapturadoTotalmente).IsRequired();
        builder.Property(resultadoPesquisa => resultadoPesquisa.AoMenosUmCapturado).IsRequired();

        builder.HasOne(resultadoPesquisa => resultadoPesquisa.Pesquisa)
            .WithMany(pesquisa => pesquisa.ResultadoPesquisas)
            .HasForeignKey(resultadoPesquisa => resultadoPesquisa.PesquisaId);
    }
}