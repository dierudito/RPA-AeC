
using Moreno.RPA_AeC.Domain.Value_Objects;

namespace Moreno.RPA_AeC.UnityTests.Domain.Value_Objects
{
    public class DateTimeExtensionsTest
    {
        private readonly Faker _faker;

        public DateTimeExtensionsTest()
        {
            _faker = new Faker();
        }

        [Theory]
        [InlineData("dd/MM/yyyy")]
        [InlineData("MM/dd/yyyy")]
        [InlineData("M/dd/yyyy")]
        [InlineData("dd MMMM yyyy")]
        [InlineData("MMMM dd")]
        [InlineData("yyyy-MM-dd")]
        [InlineData("dd MMM yyyy")]
        public void DeveConverterStringParaDataComSucesso(string format)
        {
            // Arrange
            var dataEsperada = _faker.Date.RecentDateOnly();
            var textoData = dataEsperada.ToString(format);

            // Act
            var dataRetornada = textoData.ConverterStringParaData();

            // Assert
            DateOnly.FromDateTime(dataRetornada).Should().Be(dataEsperada);
        }
        
        [Fact]
        public void DeveLancarExceptionAoConverterStringVaziaParaData()
        {
            // Arrange
            const string mensagemEsperada = "O texto da data está vazio";
            // Act / Assert
            var mensagem = Assert.Throws<Exception>(() => string.Empty.ConverterStringParaData());
            mensagem.Message.Should().BeEquivalentTo(mensagemEsperada);
        }

        [Fact]
        public void DeveLancarExceptionAoConverterStringInvalidaParaData()
        {
            // Arrange
            var textoData = _faker.Random.AlphaNumeric(10);
            var mensagemEsperada = $"Não foi possível converter o texto {textoData} para uma data";
            // Act / Assert
            var mensagem = Assert.Throws<Exception>(() => textoData.ConverterStringParaData());
            mensagem.Message.Should().BeEquivalentTo(mensagemEsperada);
        }
    }
}
