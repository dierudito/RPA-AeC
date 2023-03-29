using Moreno.RPA_AeC.Domain.Entities;

namespace Moreno.RPA_AeC.UnityTests.Domain.Entities
{
    public class ResultadoPesquisaTest
    {
        private readonly Faker _faker;

        public ResultadoPesquisaTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveDefinirDataPublicacaoComSucesso()
        {
            // Arrange
            var dataEsperada = _faker.Date.RecentDateOnly();
            var textoData = dataEsperada.ToString();
            var resultadoPesquisa = new ResultadoPesquisa(Guid.NewGuid());

            // Act
            resultadoPesquisa.DefinirDataPublicacao(textoData);

            // Assert
            resultadoPesquisa.DataPublicacao.Should().HaveValue();
            DateOnly.FromDateTime(resultadoPesquisa.DataPublicacao.Value).Should().Be(dataEsperada);
        }
    }
}
