using Moreno.RPA_AeC.Domain.Entities;
using Moreno.RPA_AeC.Domain.Interfaces;

namespace Moreno.RPA_AeC.Domain.Services
{
    public class PesquisaService : IPesquisaService
    {
        private readonly IPesquisaRepository _pesquisaRepository;

        public PesquisaService(IPesquisaRepository pesquisaRepository)
        {
            _pesquisaRepository = pesquisaRepository;
        }
        public async Task<Pesquisa> AdicionarAsync(Pesquisa pesquisa)
        {
            await _pesquisaRepository.AdicionarAsync(pesquisa);
            return pesquisa;
        }

        public async Task<Pesquisa> AtualizarAsync(Pesquisa pesquisa)
        {
            await _pesquisaRepository.AtualizarAsync(pesquisa);
            return pesquisa;
        }

        public async Task RemoverAsync(Guid id)
        {
            await _pesquisaRepository.RemoverAsync(id);
        }

        public void Dispose() => _pesquisaRepository.Dispose();
    }
}
