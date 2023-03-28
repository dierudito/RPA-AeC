using Moq;
using Moq.AutoMock;
using Moreno.RPA_AeC.Domain.Entities;
using Moreno.RPA_AeC.Domain.Interfaces;
using Moreno.RPA_AeC.Domain.Services;

namespace Moreno.RPA_AeC.UnityTests.Domain.Services
{
    public class PesquisaServiceTest
    {
        private readonly Faker _faker;
        private readonly Mock<IPesquisaRepository> _pesquisaRepository;
        private readonly PesquisaService _pesquisaService;

        public PesquisaServiceTest()
        {
            _faker = new Faker();
            var mocker = new AutoMocker();

            _pesquisaRepository = mocker.GetMock<IPesquisaRepository>();
            _pesquisaService = mocker.CreateInstance<PesquisaService>();
        }

        [Fact]
        public async Task DeveAdicionarPesquisaComSucesso()
        {
            // Arrange
            var pesquisa = new Pesquisa(_faker.Lorem.Text());

            // Act
            var retorno = await _pesquisaService.AdicionarAsync(pesquisa);

            // Assert
            retorno.Should().BeEquivalentTo(pesquisa);
            _pesquisaRepository
                .Verify(p => p.AdicionarAsync(It.Is<Pesquisa>(
                    x => x.DataPesquisa == pesquisa.DataPesquisa && 
                         x.Id == pesquisa.Id && 
                         x.Termo == pesquisa.Termo)), Times.Once);
        }

        [Fact]
        public async Task DeveAtualizarPesquisaComSucesso()
        {
            // Arrange
            var pesquisa = new Pesquisa(_faker.Lorem.Text());

            // Act
            var retorno = await _pesquisaService.AtualizarAsync(pesquisa);

            // Assert
            retorno.Should().BeEquivalentTo(pesquisa);
            _pesquisaRepository
                .Verify(p => p.AtualizarAsync(It.Is<Pesquisa>(
                    x => x.DataPesquisa == pesquisa.DataPesquisa &&
                         x.Id == pesquisa.Id &&
                         x.Termo == pesquisa.Termo)), Times.Once);
        }

        [Fact]
        public async Task DeveRemoverPesquisaComSucesso()
        {
            // Arrange
            var pesquisa = new Pesquisa(_faker.Lorem.Text());

            // Act
            await _pesquisaService.RemoverAsync(pesquisa.Id);

            // Assert
            _pesquisaRepository
                .Verify(p => p.RemoverAsync(pesquisa.Id), Times.Once);
        }
    }
}
