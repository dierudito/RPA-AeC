namespace Moreno.RPA_AeC.Domain.Interfaces;

public interface IUnitOfWork
{
    int Commit();
    Task<int> CommitAsync();
}