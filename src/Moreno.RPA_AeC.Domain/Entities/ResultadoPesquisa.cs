using Moreno.RPA_AeC.Domain.Value_Objects;

namespace Moreno.RPA_AeC.Domain.Entities;

public class ResultadoPesquisa : Entity
{
    public Guid PesquisaId { get; private set; }
    public string Titulo { get; private set; }
    public string Area { get; private set; }
    public string Autor { get; private set; }
    public string Descricao { get; private set; }
    public DateTime? DataPublicacao { get; private set; }
    public bool CapturadoTotalmente { get; private set; }
    public bool AoMenosUmCapturado { get; private set; }

    // Necessário para navegação do EF
    public virtual Pesquisa Pesquisa { get; private set; }

    public ResultadoPesquisa()
    {

    }

    public ResultadoPesquisa(Guid pesquisaId)
    {
        PesquisaId = pesquisaId;
        CapturadoTotalmente = true;
        AoMenosUmCapturado = true;
    }

    public void DefinirTitulo(string titulo)
    {
        Titulo = titulo;
    }
    public void DefinirArea(string area)
    {
        Area = area;
    }
    public void DefinirAutor(string autor)
    {
        Autor = autor;
    }
    public void DefinirDescricao(string descricao)
    {
        Descricao = descricao;
    }
    public void DefinirDataPublicacao(string dataPublicacao)
    {
        DataPublicacao = dataPublicacao.ConverterStringParaData();
    }

    public void DefinirComoCapturadoTotalmente()
    {
        CapturadoTotalmente = !string.IsNullOrEmpty(Area) &&
            !string.IsNullOrEmpty(Autor) &&
            !string.IsNullOrEmpty(Descricao) &&
            !string.IsNullOrEmpty(Titulo) &&
            DataPublicacao.HasValue;
    }

    public void DefinirAoMenosUmCapturado()
    {
        AoMenosUmCapturado = !string.IsNullOrEmpty(Area) ||
            !string.IsNullOrEmpty(Autor) ||
            !string.IsNullOrEmpty(Descricao) ||
            !string.IsNullOrEmpty(Titulo) ||
            DataPublicacao.HasValue;
    }
}
