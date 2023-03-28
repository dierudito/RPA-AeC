namespace Moreno.RPA_AeC.Domain.Entities;

public class LogPesquisa : Entity
{
    public Guid PesquisaId { get; private set; }
    public string Descricao { get; private set; }
    public DateTime DataLog { get; private set; }

    // Necessário para navegação do EF
    public virtual Pesquisa Pesquisa { get; private set; }

    public LogPesquisa()
    {

    }

    public LogPesquisa(Guid pesquisaId, string descricao)
    {
        PesquisaId = pesquisaId;
        Descricao = descricao;
        DataLog = DateTime.Now;
    }
}
