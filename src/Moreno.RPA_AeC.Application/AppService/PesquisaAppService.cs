using DomainValidationCore.Validation;
using Moreno.RPA_AeC.Application.AppService.Base;
using Moreno.RPA_AeC.Application.Interfaces;
using Moreno.RPA_AeC.Application.ViewModels;
using Moreno.RPA_AeC.Domain.Entities;
using Moreno.RPA_AeC.Domain.Interfaces;
using Moreno.RPA_AeC.Infra.Rpa.Base;
using Moreno.RPA_AeC.Infra.Rpa.Interfaces;
using OpenQA.Selenium;

namespace Moreno.RPA_AeC.Application.AppService;

public class PesquisaAppService : BaseAppService, IPesquisaAppService
{
    private const string ENDERECO_SITE = "https://www.aec.com.br/";
    private readonly IPesquisaService _pesquisaService;
    private readonly ISeleniumRpa _seleniumRpa;
    private ValidationResult _valdationResult;

    public PesquisaAppService(IPesquisaService pesquisaService, IUnitOfWork uow, ISeleniumRpa seleniumRpa) : base(uow)
    {
        _pesquisaService = pesquisaService;
        _seleniumRpa = seleniumRpa;
    }

    public async Task<RelatorioPesquisaViewModel> PesquisarTermoAsync(string termo)
    {
        var relatorio = new RelatorioPesquisaViewModel { TermoPesquisado = termo };
        _valdationResult = relatorio.ValidationResult;

        var pesquisa = await ObterDadosDoSite(termo);
        relatorio.DataTermino = DateTime.Now;

        relatorio.QuantidadeRegistrosEncontrados = pesquisa.ResultadoPesquisas.Count;
        relatorio.QuantidadeRegistrosNaoGravados = 
            pesquisa.ResultadoPesquisas.Count(resultado => !resultado.AoMenosUmCapturado);
        relatorio.QuantidadeRegistrosGravadosComRessalvas = 
            pesquisa.ResultadoPesquisas.Count(resultado => resultado.AoMenosUmCapturado && 
                                                          !resultado.CapturadoTotalmente);
        relatorio.QuantidadeRegistrosGravadosComSucesso =
            pesquisa.ResultadoPesquisas.Count(resultado => resultado.CapturadoTotalmente);

        EscreverLogsDaPesquisa(pesquisa);
        pesquisa = await _pesquisaService.AdicionarAsync(pesquisa).ConfigureAwait(false);

        try
        {
            if ((await CommitAsync()) == 0)
            {
                AdicionarErrosValidacao(_valdationResult, "", "Ocorreu um erro no momento de salvar os dados no banco.");
            }
        }
        catch (Exception e)
        {
            AdicionarErrosValidacao(_valdationResult, "", "Ocorreu um erro no momento de salvar os dados no banco.");
        }
        relatorio.ValidationResult = _valdationResult;
        return relatorio;
    }

    public void Dispose()
    {
        _pesquisaService.Dispose();
    }

    private async Task<Pesquisa> ObterDadosDoSite(string termo)
    {
        var pesquisa = new Pesquisa(termo);
        try
        {
            const string chaveInputPesquisa = "s";
            const string chaveFormPesquisa = "form";
            const string chaveElementoRetornoPesquisa = "/html/body/main/div[2]/div/strong/div[1]/div/div/div/a";
            await _seleniumRpa.NavegarParaUrl(ENDERECO_SITE);
            var valorAtribuito = await _seleniumRpa.AtribuirValor(TipoElementoEnum.Name, chaveInputPesquisa, termo, 0);

            if (!valorAtribuito)
            {
                AdicionarErrosValidacao(_valdationResult, "Atribuir valor", "Erro ao setar o termo da pesquisa no site alvo!");
                return pesquisa;
            }

            var comandoEnviado = await _seleniumRpa.EnviarComando(TipoElementoEnum.Id, chaveFormPesquisa, TipoComandoEnum.Submit);

            if (!comandoEnviado)
            {
                AdicionarErrosValidacao(_valdationResult, "Enviar Comando", "Erro ao submeter a pesquisa no site alvo!");
                return pesquisa;
            }

            var elementoQueRetornaOsDadosDaPesquisa =
                await _seleniumRpa.ObterListaDeElementos(TipoElementoEnum.XPath, chaveElementoRetornoPesquisa);

            foreach (var item in elementoQueRetornaOsDadosDaPesquisa.Select((value, i) => new { i, value }))
            {
                var resultado = await ObterConteudoDoResultadoDaPesquisa(item.value, item.i);
                var resultadoPesquisa = new ResultadoPesquisa(pesquisa.Id);
                resultadoPesquisa.DefinirAutor(resultado.autor);
                resultadoPesquisa.DefinirTitulo(resultado.titulo);
                resultadoPesquisa.DefinirDescricao(resultado.descricao);
                resultadoPesquisa.DefinirArea(resultado.area);
                resultadoPesquisa.DefinirDataPublicacao(resultado.data);
                ValidarDadosDaEntidadeResultadoPesquisa(resultadoPesquisa);
                pesquisa.AdicionarResultadoPesquisa(resultadoPesquisa);
            }

            if (elementoQueRetornaOsDadosDaPesquisa == null)
            {
                AdicionarErrosValidacao(_valdationResult, "Retorno pesquisa", $"Nenhum dado coletado para a pesquisa realizada para o termo: {termo}");
                return pesquisa;
            }
            return pesquisa;
        }
        catch (Exception e)
        {
            AdicionarErrosValidacao(_valdationResult, "Obter Dados Do Site",
                $"Ocorreu um erro inesperado ao obter os dados do site {ObterDetalhesDaException(e)}");
            return pesquisa;
        }
    }

    private void ValidarDadosDaEntidadeResultadoPesquisa(ResultadoPesquisa resultadoPesquisa)
    {
        resultadoPesquisa.DefinirComoCapturadoTotalmente();
        resultadoPesquisa.DefinirAoMenosUmCapturado();

        if (resultadoPesquisa.CapturadoTotalmente) return;

        if (string.IsNullOrEmpty(resultadoPesquisa.Area))
            AdicionarErrosValidacao(_valdationResult, "Area", $"Area definida como vazio para a pesquisa {resultadoPesquisa.PesquisaId}");
        if (string.IsNullOrEmpty(resultadoPesquisa.Autor))
            AdicionarErrosValidacao(_valdationResult, "Autor", $"Autor definido como vazio para a pesquisa {resultadoPesquisa.PesquisaId}");
        if (!resultadoPesquisa.DataPublicacao.HasValue)
            AdicionarErrosValidacao(_valdationResult, "Data Publicacao", $"Data Publicacao nao definida para a pesquisa {resultadoPesquisa.PesquisaId}");
        if (string.IsNullOrEmpty(resultadoPesquisa.Descricao))
            AdicionarErrosValidacao(_valdationResult, "Descricao", $"Descricao definida como vazio para a pesquisa {resultadoPesquisa.PesquisaId}");
        if (string.IsNullOrEmpty(resultadoPesquisa.Titulo))
            AdicionarErrosValidacao(_valdationResult, "Titulo", $"Titulo definido como vazio para a pesquisa {resultadoPesquisa.PesquisaId}");
    }

    private async Task<(string titulo, string area, string descricao, string autor, string data)> 
        ObterConteudoDoResultadoDaPesquisa(IWebElement webElement, int index)
    {
        try
        {
            var titulo = await ObterTituloAsync(webElement);
            var area = await ObterAreaAsync(webElement);
            var descricao = await ObterDescricaoAsync(webElement);
            var dadosPublicacao = await ObterDadosPublicacaoAsync(webElement);
            var (autor, data) = SepararDadosDaPublicacao(dadosPublicacao);

            return (titulo, area, descricao, autor, data);
        }
        catch (Exception e)
        {
            AdicionarErrosValidacao(_valdationResult, "Obter Conteudo Do Resultado Da Pesquisa",
                $"Ocorreu um erro inesperado ao obter o conteudo do resultado {index} da pesquisa do site {ObterDetalhesDaException(e)}");
            return (string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }
    }

    private async Task<string> ObterTituloAsync(IWebElement webElement)
    {
        try
        {
            const string chaveElementoTitulo = "h3";
            var elementoTitulo = await _seleniumRpa.ObterElemento(webElement, TipoElementoEnum.TagName, chaveElementoTitulo, 0);
            var titulo = await _seleniumRpa.ObterConteudoDoAtributo(elementoTitulo, TipoAtributoEnum.text);
            return titulo;
        }
        catch (Exception e)
        {
            AdicionarErrosValidacao(_valdationResult, "Obter Titulo",
                $"Erro ao obter titulo {ObterDetalhesDaException(e)}");
            return string.Empty;
        }
    }

    private async Task<string> ObterAreaAsync(IWebElement webElement)
    {
        try
        {
            const string chaveElementoArea = "span";
            var elementoArea = await _seleniumRpa.ObterElemento(webElement, TipoElementoEnum.TagName, chaveElementoArea, 0);
            var area = await _seleniumRpa.ObterConteudoDoAtributo(elementoArea, TipoAtributoEnum.text);
            return area;
        }
        catch (Exception e)
        {
            AdicionarErrosValidacao(_valdationResult, "Obter Area",
                $"Erro ao obter area {ObterDetalhesDaException(e)}");
            return string.Empty;
        }
    }

    private async Task<string> ObterDescricaoAsync(IWebElement webElement)
    {
        try
        {
            const string chaveElementoDescricao = "p";
            var elementoDescricao = await _seleniumRpa.ObterElemento(webElement, TipoElementoEnum.TagName, chaveElementoDescricao, 0);
            var descricao = await _seleniumRpa.ObterConteudoDoAtributo(elementoDescricao, TipoAtributoEnum.text);
            return descricao;
        }
        catch (Exception e)
        {
            AdicionarErrosValidacao(_valdationResult, "Obter Descricao",
                $"Erro ao obter descricao {ObterDetalhesDaException(e)}");
            return string.Empty;
        }
    }

    private async Task<string> ObterDadosPublicacaoAsync(IWebElement webElement)
    {
        try
        {
            const string chaveElementoDadosPublicacao = "span";
            var elementoDadosPublicacao = await _seleniumRpa.ObterElemento(webElement, TipoElementoEnum.TagName, chaveElementoDadosPublicacao, 1);
            var dadosPublicacao = await _seleniumRpa.ObterConteudoDoAtributo(elementoDadosPublicacao, TipoAtributoEnum.text);
            return dadosPublicacao;
        }
        catch (Exception e)
        {
            AdicionarErrosValidacao(_valdationResult, "Obter Dados da Publicação",
                $"Erro ao obter dados da publicação autor/data {ObterDetalhesDaException(e)}");
            return string.Empty;
        }
    }

    private (string autor, string data) SepararDadosDaPublicacao(string dadosPublicacao)
    {
        const string marcaSeparacaoAutorData = " em ";
        const string marcaInicioAutor = " por ";
        var autor = string.Empty;
        var data = string.Empty;

        if (string.IsNullOrWhiteSpace(dadosPublicacao)) return (autor, data);
        int inicioDaData;
        int terminoAutor;
        if (dadosPublicacao.Contains(marcaSeparacaoAutorData))
        {
            inicioDaData = dadosPublicacao.IndexOf(marcaSeparacaoAutorData) + marcaSeparacaoAutorData.Length;
            terminoAutor = inicioDaData - marcaSeparacaoAutorData.Length;
        }
        else if (dadosPublicacao.Contains('/'))
        {
            inicioDaData = dadosPublicacao.IndexOf('/') - 2;
            terminoAutor = inicioDaData;
        }
        else
        {
            AdicionarErrosValidacao(_valdationResult, "Seprar Dados da Publicacao", $"Nao foi possivel separar os dados da publicacao {dadosPublicacao}");
            return (autor, data);
        }

        data = dadosPublicacao[inicioDaData..];

        int inicioAutor;
        if (dadosPublicacao.Contains(marcaInicioAutor)) inicioAutor = dadosPublicacao.IndexOf(marcaInicioAutor) + marcaInicioAutor.Length;
        else
        {
            AdicionarErrosValidacao(_valdationResult, "Seprar Dados da Publicacao", $"Nao foi possivel separar o autor dos dados da publicacao {dadosPublicacao}");
            return (autor, data);
        }

        autor = dadosPublicacao.Substring(inicioAutor, terminoAutor- inicioAutor);
        return (autor, data);
    }

    private void EscreverLogsDaPesquisa(Pesquisa pesquisa)
    {
        foreach (var erro in _valdationResult.Errors)
        {
            pesquisa.AdicionarLogPesquisa(new LogPesquisa(pesquisa.Id, $"Nome: {erro.Name} | Descricao: {erro.Message}"));
        }
    }
}
