using Moreno.RPA_AeC.Domain.Entities;

namespace Moreno.RPA_AeC.Domain.Interfaces;

public interface IRepositoryWrite<TEntity> : IDisposable where TEntity : Entity
{
    Task AdicionarAsync(TEntity obj);
    Task AtualizarAsync(TEntity obj);
    Task RemoverAsync(Guid id);
    Task<int> SaveChangesAsync();
}