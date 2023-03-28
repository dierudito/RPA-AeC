namespace Moreno.RPA_AeC.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public Entity()
    {
        Id = new Guid();
    }
}
