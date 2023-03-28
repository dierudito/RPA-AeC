using Moreno.RPA_AeC.Domain.Entities;

namespace Moreno.RPA_AeC.Domain.Interfaces;

public interface IResultadoPesquisaRepository : IRepositoryRead<ResultadoPesquisa>, IRepositoryWrite<ResultadoPesquisa>
{
    Task<IEnumerable<ResultadoPesquisa>> ObterResultadosDaPesquisaAsync(Guid pesquisaId);
}