using Moq;
using Moq.AutoMock;
using Moreno.RPA_AeC.Application.AppService;
using Moreno.RPA_AeC.Domain.Entities;
using Moreno.RPA_AeC.Domain.Interfaces;
using Moreno.RPA_AeC.Infra.Rpa.Base;
using Moreno.RPA_AeC.Infra.Rpa.Interfaces;
using Moreno.RPA_AeC.UnityTests.Shared;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Moreno.RPA_AeC.UnityTests.Application.AppService;

public class PesquisaAppServiceTest
{
    private readonly Faker _faker;
    private readonly Mock<IPesquisaService> _pesquisaService;
    private readonly Mock<ISeleniumRpa> _seleniumRpa;
    private readonly Mock<IUnitOfWork> _uow;
    private readonly PesquisaAppService _appService;

    public PesquisaAppServiceTest()
    {
        _faker = new Faker();
        var mocker = new AutoMocker();

        _pesquisaService = mocker.GetMock<IPesquisaService>();
        _seleniumRpa = mocker.GetMock<ISeleniumRpa>();
        _uow = mocker.GetMock<IUnitOfWork>();
        _appService = mocker.CreateInstance<PesquisaAppService>();
    }

    [Fact]
    public async Task DevePesquisarTermoComSucesso()
    {
        // Arrange
        var termo = _faker.Lorem.Word();
        IWebElement webElement = new WebElementUnityTest();
        var listaElementos = new List<IWebElement> { webElement };
        var readOnly = new ReadOnlyCollection<IWebElement>(listaElementos);
        var dataPublicacao = _faker.Date.RecentDateOnly();
        var autor = _faker.Person.FullName;
        var dadosPublicacao = $"{_faker.Lorem.Word()} por {autor} em {dataPublicacao:dd/MM/yyyy}";

        _seleniumRpa
            .Setup(s => s.AtribuirValor(It.IsAny<TipoElementoEnum>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(true);
        _seleniumRpa
            .Setup(s => s.EnviarComando(It.IsAny<TipoElementoEnum>(), It.IsAny<string>(), It.IsAny<TipoComandoEnum>(), It.IsAny<int>()))
            .ReturnsAsync(true);
        _seleniumRpa
            .Setup(s => s.ObterListaDeElementos(It.IsAny<TipoElementoEnum>(), It.IsAny<string>()))
            .ReturnsAsync(readOnly);

        _seleniumRpa
            .Setup(s => s.ObterElemento(It.IsAny<IWebElement>(), It.IsAny<TipoElementoEnum>(), It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(webElement);

        _seleniumRpa
            .Setup(s => s.ObterConteudoDoAtributo(It.IsAny<IWebElement>(), It.IsAny<TipoAtributoEnum>()))
            .ReturnsAsync(dadosPublicacao);
        _uow.Setup(x => x.CommitAsync()).ReturnsAsync(1);

        // Act
        var relatorio = await _appService.PesquisarTermoAsync(termo);

        // Assert
        _pesquisaService.Verify(p => p.AdicionarAsync(It.IsAny<Pesquisa>()), Times.Once);
    }

    [Fact]
    public async Task DevePesquisarTermoComSucessoQuandoValorNaoForAtribuido()
    {
        // Arrange
        var termo = _faker.Lorem.Word();

        _seleniumRpa
            .Setup(s => s.AtribuirValor(It.IsAny<TipoElementoEnum>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(false);
        _uow.Setup(x => x.CommitAsync()).ReturnsAsync(1);

        // Act
        var relatorio = await _appService.PesquisarTermoAsync(termo);

        // Assert
        _pesquisaService.Verify(p => p.AdicionarAsync(It.IsAny<Pesquisa>()), Times.Once);
    }

    [Fact]
    public async Task DevePesquisarTermoComSucessoQuandoComandoNaoForEnviado()
    {
        // Arrange
        var termo = _faker.Lorem.Word();

        _seleniumRpa
            .Setup(s => s.AtribuirValor(It.IsAny<TipoElementoEnum>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(true);
        _seleniumRpa
            .Setup(s => s.EnviarComando(It.IsAny<TipoElementoEnum>(), It.IsAny<string>(), It.IsAny<TipoComandoEnum>(), It.IsAny<int>()))
            .ReturnsAsync(false);
        _uow.Setup(x => x.CommitAsync()).ReturnsAsync(1);

        // Act
        var relatorio = await _appService.PesquisarTermoAsync(termo);

        // Assert
        _pesquisaService.Verify(p => p.AdicionarAsync(It.IsAny<Pesquisa>()), Times.Once);
    }

    [Fact]
    public async Task DeveAdicionarRegistroAoLogQuandoAoPesquisaTermoNaoEncontrarElementos()
    {
        // Arrange
        var termo = _faker.Lorem.Word();
        var descricaoLog = $"Nome: Retorno pesquisa | Descricao: Nenhum dado coletado para a pesquisa realizada para o termo: {termo}";

        _seleniumRpa
            .Setup(s => s.AtribuirValor(It.IsAny<TipoElementoEnum>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(true);
        _seleniumRpa
            .Setup(s => s.EnviarComando(It.IsAny<TipoElementoEnum>(), It.IsAny<string>(), It.IsAny<TipoComandoEnum>(), It.IsAny<int>()))
            .ReturnsAsync(true);
        _uow.Setup(x => x.CommitAsync()).ReturnsAsync(1);

        // Act
        var relatorio = await _appService.PesquisarTermoAsync(termo);

        // Assert
        _pesquisaService
            .Verify(p => p.AdicionarAsync(It.Is<Pesquisa>(x =>
                x.LogsPesquisa.Any() && x.LogsPesquisa.FirstOrDefault().Descricao.Equals(descricaoLog))), Times.Once);
    }
}
