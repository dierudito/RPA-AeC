using Moreno.RPA_AeC.Domain.Entities;

namespace Moreno.RPA_AeC.Domain.Interfaces;

public interface IPesquisaService : IDisposable
{
    Task<Pesquisa> AdicionarAsync(Pesquisa pesquisa);
    Task<Pesquisa> AtualizarAsync(Pesquisa pesquisa);
    Task RemoverAsync(Guid id);
}