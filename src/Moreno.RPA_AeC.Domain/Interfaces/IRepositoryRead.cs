using Moreno.RPA_AeC.Domain.Entities;
using System.Linq.Expressions;

namespace Moreno.RPA_AeC.Domain.Interfaces;

public interface IRepositoryRead<TEntity> : IDisposable where TEntity : Entity
{
    Task<TEntity> ObterPorIdAsync(Guid id);
    Task<IEnumerable<TEntity>> ObterTodosAsync();
    Task<IEnumerable<TEntity>> ObterTodosPaginadoAsync(int s, int t);
    Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate);
}
