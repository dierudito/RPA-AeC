namespace Moreno.RPA_AeC.Domain.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
    Task BeginTransactionAsync();
    Task RollbackAsync();
    Task<bool> SaveChangesAsync();
}