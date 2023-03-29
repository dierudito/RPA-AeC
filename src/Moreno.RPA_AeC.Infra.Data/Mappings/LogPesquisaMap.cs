using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moreno.RPA_AeC.Domain.Entities;

namespace Moreno.RPA_AeC.Infra.Data.Mappings;

[ExcludeFromCodeCoverage]
public class LogPesquisaMap : IEntityTypeConfiguration<LogPesquisa>
{
    public void Configure(EntityTypeBuilder<LogPesquisa> builder)
    {
        builder.ToTable("Pesquisas_Logs");

        builder.Property(logPesquisa => logPesquisa.Descricao).HasColumnName("varchar").HasMaxLength(500).IsRequired();
        builder.Property(logPesquisa => logPesquisa.DataCadastro).IsRequired();

        builder.HasOne(logPesquisa => logPesquisa.Pesquisa)
            .WithMany(pesquisa => pesquisa.LogsPesquisa)
            .HasForeignKey(logPesquisa => logPesquisa.PesquisaId);
    }
}
