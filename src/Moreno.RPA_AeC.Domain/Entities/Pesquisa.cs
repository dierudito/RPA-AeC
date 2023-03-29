namespace Moreno.RPA_AeC.Domain.Entities;

public class Pesquisa : Entity
{
    public string Termo { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public virtual ICollection<ResultadoPesquisa> ResultadoPesquisas { get; private set; }
    public virtual ICollection<LogPesquisa> LogsPesquisa { get; private set; }

    public Pesquisa()
    {

    }

    public Pesquisa(string termo)
    {
        Termo = termo;
        DataCadastro = new DateTime();
        ResultadoPesquisas = new List<ResultadoPesquisa>();
        LogsPesquisa = new List<LogPesquisa>();
    }

    public void AdicionarResultadoPesquisa(ResultadoPesquisa resultadoPesquisa)
    {
        ResultadoPesquisas.Add(resultadoPesquisa);
    }

    public void AdicionarLogPesquisa(LogPesquisa logPesquisa)
    {
        LogsPesquisa.Add(logPesquisa);
    }
}
