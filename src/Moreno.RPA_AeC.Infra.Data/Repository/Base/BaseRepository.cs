using Microsoft.EntityFrameworkCore;
using Moreno.RPA_AeC.Domain.Entities;
using Moreno.RPA_AeC.Domain.Interfaces;
using Moreno.RPA_AeC.Infra.Data.Context.Entity;

namespace Moreno.RPA_AeC.Infra.Data.Repository.Base;

[ExcludeFromCodeCoverage]
public abstract class BaseRepository<TEntity> : IDisposable, IRepositoryRead<TEntity>, IRepositoryWrite<TEntity> where TEntity : Entity, new()
{
    protected RPA_AeCDbContext _db;
    protected DbSet<TEntity> _dbSet;

    public BaseRepository(RPA_AeCDbContext db)
    {
        _db = db;
        _dbSet = _db.Set<TEntity>();
    }

    public async Task AdicionarAsync(TEntity entidade)
    {
        await _db.Set<TEntity>().AddAsync(entidade);
    }

    public async Task AtualizarAsync(TEntity entidade)
    {
        _db.Set<TEntity>().Update(entidade);
    }

    public async Task RemoverAsync(Guid id)
    {
        var entity = new TEntity { Id = id };
        _dbSet.Remove(entity);
    }

    public async Task<TEntity> ObterPorIdAsync(Guid id)
    {
        TEntity result = await _db.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
        return result;
    }

    public async Task<IEnumerable<TEntity>> ObterTodosAsync()
        => await _db.Set<TEntity>().ToListAsync().ConfigureAwait(false);

    public async Task<int> SaveChangesAsync()
    {
        var retorno = await _db.SaveChangesAsync().ConfigureAwait(false);
        return retorno;
    }

    public void Dispose()
    {
        _db?.Dispose();
        GC.SuppressFinalize(this);
    }
}
