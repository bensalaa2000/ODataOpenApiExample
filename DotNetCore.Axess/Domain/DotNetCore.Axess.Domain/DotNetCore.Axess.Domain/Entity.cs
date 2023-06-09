namespace DotNetCore.Axess.Domain;

public abstract class Entity<TId> : Base<Entity<TId>>
{
    public TId Id { get; protected set; }

    protected sealed override IEnumerable<object> Equals() { yield return Id; }

    protected Entity(TId id)
    {
        Id = id;
    }
}
