using Moreno.RPA_AeC.Domain.Entities;

namespace Moreno.RPA_AeC.Domain.Interfaces;

public interface IPesquisaRepository : IRepositoryRead<Pesquisa>, IRepositoryWrite<Pesquisa>
{
}
