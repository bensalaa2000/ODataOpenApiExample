namespace DotNetCore.Axess.Domain;

public abstract class Event
{
    public Guid Id { get; } = Guid.NewGuid();

    public DateTime DateTime { get; } = DateTime.UtcNow;
}
