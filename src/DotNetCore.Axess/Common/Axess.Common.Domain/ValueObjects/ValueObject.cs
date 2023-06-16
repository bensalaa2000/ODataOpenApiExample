using Axess.Common.Domain.Entities;

namespace Axess.Common.Domain.ValueObjects;

public abstract class ValueObject : Base<ValueObject> { }

public abstract class ValueObject<T> : ValueObject
{
    protected ValueObject(T value) => Value = value;

    public T Value { get; }

    protected sealed override IEnumerable<object> Equals() { yield return Value; }
}
