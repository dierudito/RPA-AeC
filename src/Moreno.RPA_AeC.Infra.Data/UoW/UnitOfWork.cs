using Moreno.RPA_AeC.Domain.Interfaces;
using Moreno.RPA_AeC.Infra.Data.Context.Entity;

namespace Moreno.RPA_AeC.Infra.Data.UoW;

[ExcludeFromCodeCoverage]
public class UnitOfWork : IUnitOfWork
{
    private readonly RPA_AeCDbContext _db;

    public UnitOfWork(RPA_AeCDbContext db)
    {
        _db = db;
    }

    public int Commit()
    {
        return _db.SaveChanges();
    }

    public async Task<int> CommitAsync() => await _db.SaveChangesAsync().ConfigureAwait(false);
}
