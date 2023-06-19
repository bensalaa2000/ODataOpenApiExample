using Axess.Common.Domain.Entities;

namespace Axess.Domain;

public abstract class Entity : Base<Entity>
{
	public Guid Id { get; protected set; }

	protected sealed override IEnumerable<object> Equals() { yield return Id; }

	protected Entity(Guid id)
	{
		Id = id;
	}
}
