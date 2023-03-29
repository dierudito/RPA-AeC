using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using Moreno.RPA_AeC.Application.Interfaces;
using Moreno.RPA_AeC.Application.ViewModels;
using Moreno.RPA_AeC.WebApi.Bff.Controllers;

namespace Moreno.RPA_AeC.UnityTests.Presentation.Controllers;

public class RpaControllerTest
{
    private readonly Faker _faker;
    private readonly Mock<IPesquisaAppService> _pesquisaAppService;
    private readonly RpaController _controller;

    public RpaControllerTest()
    {
        _faker = new Faker();
        var mocker = new AutoMocker();

        _pesquisaAppService = mocker.GetMock<IPesquisaAppService>();
        _controller = mocker.CreateInstance<RpaController>();
    }

    [Fact]
    public async Task DevePesquisarTermoComSucesso()
    {
        // Arrange
        var termo = _faker.Lorem.Word();
        const int statusCodeEsperado = StatusCodes.Status200OK;
        var relatorioEsperado = new RelatorioPesquisaViewModel
        {
            DataTermino = _faker.Date.Future(0),
            QuantidadeRegistrosEncontrados = _faker.Random.Number(50, 100),
            QuantidadeRegistrosGravadosComRessalvas = _faker.Random.Number(),
            QuantidadeRegistrosGravadosComSucesso = _faker.Random.Number(50, 100),
            QuantidadeRegistrosNaoGravados = _faker.Random.Number(),
            TermoPesquisado = termo
        };

        _pesquisaAppService.Setup(x => x.PesquisarTermoAsync(termo)).ReturnsAsync(relatorioEsperado);

        // Act
        var retorno = await _controller.PesquisarTermoAsync(termo);
        var response = (ObjectResult)retorno;
        var value = (RelatorioPesquisaViewModel)response.Value;
        var statusCode = response.StatusCode;

        // Assert
        value.Should().BeEquivalentTo(relatorioEsperado);
        statusCode.Should().Be(statusCodeEsperado);
    }
}
